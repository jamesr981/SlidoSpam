using System.Net.Http.Headers;
using Bogus;
using Newtonsoft.Json;
using RestSharp;
using SlidoSpam.Extensions;
using SlidoSpam.Payloads;
using SlidoSpam.Responses;

public static class Program
{
    private const string EventUuid = "01a5ca23-84a2-4046-9435-aff13dc672dc";
    private const long QuestionId = 57956953;
    private const int MaxQuestionLength = 160;
    
    public static async Task Main()
    {
        for (var i = 0; i < 100; i++)
        {
            using var restClient = GetRestClient(EventUuid);

            var authResponse = await Authenticate(restClient);
            if (authResponse is null) return;
            
            //Need to wait 2 seconds before next api call to let server process auth event
            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine("Authenticated. Access Token: {0}", authResponse.AccessToken);
        
            var user = await GetUser(restClient, authResponse);
            var @event = await GetEvent(restClient, authResponse, EventUuid);
            if (user is null || @event is null) return;
            
            var postAs = new Faker().Name.FirstName();
            await CreateQuestion(restClient, authResponse, @event, postAs);

        }
    }

    private static async Task LikeQuestion(RestClient client, AuthResponse authResponse, long questionId)
    {
        var likePayload = new LikePayload
        {
            Like = true
        };
        
        var likeRequest = new RestRequest($"questions/{questionId}/like", Method.Post);
        likeRequest.AddJsonBody(likePayload);
        AddAuthenticationHeaders(likeRequest, authResponse);
        await client.PostAsync(likeRequest);
    }

    private static async Task LikeQuestions(RestClient client, AuthResponse authResponse, IEnumerable<long> questionIds)
    {
        var tasks = questionIds.Select(questionId => LikeQuestion(client, authResponse, questionId)).ToArray();
        await Task.WhenAll(tasks);
    }

    private static async Task CreateQuestion(RestClient client, AuthResponse authResponse, EventResponse eventResponse, string postAs = "")
    {
        var questionPayload = JsonConvert.SerializeObject(new QuestionPayload
        {
            Text = new Faker().Rant.Review().Truncate(MaxQuestionLength),
            EventId = eventResponse.EventId,
            IsAnonymous = string.IsNullOrWhiteSpace(postAs),
            EventSectionId = eventResponse.Sections.First().EventSectionId,
        });
        
        var createRequest = new RestRequest("questions", Method.Post);
        AddAuthenticationHeaders(createRequest, authResponse);
        createRequest.AddJsonBody(questionPayload);
        var postTask = client.PostAsync(createRequest);

        if (string.IsNullOrWhiteSpace(postAs)) return;

        var updateNameTask = UpdateUserName(client, authResponse, postAs);
        await Task.WhenAll(postTask, updateNameTask);
    }

    private static async Task<UserResponse?> GetUser(RestClient client, AuthResponse authResponse)
    {
        var userRequest = new RestRequest("user");
        AddAuthenticationHeaders(userRequest, authResponse);
        var resp = await client.GetAsync(userRequest);
        return JsonConvert.DeserializeObject<UserResponse>(resp.Content!);
    }

    private static async Task<EventResponse?> GetEvent(RestClient client, AuthResponse authResponse, string eventUuid)
    {
        var eventRequest = new RestRequest();
        AddAuthenticationHeaders(eventRequest, authResponse);
        var resp = await client.GetAsync(eventRequest);
        return JsonConvert.DeserializeObject<EventResponse>(resp.Content!);
    }
    
    private static async Task<IEnumerable<QuestionResponse>> GetQuestions(RestClient client, AuthResponse authResponse)
    {
        var questionsRequest = new RestRequest("questions");
        AddAuthenticationHeaders(questionsRequest, authResponse);
        var resp = await client.GetAsync(questionsRequest);
        return JsonConvert.DeserializeObject<QuestionResponse[]>(resp.Content!) ?? Array.Empty<QuestionResponse>();
    }

    private static async Task UpdateUserName(RestClient client, AuthResponse authResponse, string name)
    {
        var updateNameRequest = new RestRequest("user", Method.Put);
        AddAuthenticationHeaders(updateNameRequest, authResponse);
        updateNameRequest.AddJsonBody($"{{\"name\":\"{name}\"}}");
        await client.PutAsync(updateNameRequest);
    }

    private static readonly string AuthPayload = JsonConvert.SerializeObject(new AuthPayload
    {
        InitialAppViewer = "browser--other",
        GrantedConsents = new[] { "StoreEssentialCookies" }
    });

    private static async Task<AuthResponse?> Authenticate(RestClient client)
    {
        var authRequest = new RestRequest("auth?attempt=1", Method.Post);
        authRequest.AddBody(AuthPayload);

        var resp = await client.PostAsync(authRequest);
        return JsonConvert.DeserializeObject<AuthResponse>(resp.Content!);
    }

    private static void AddAuthenticationHeaders(RestRequest request, AuthResponse authResponse)
    {
        request.AddHeader("authorization",  "Bearer " + authResponse.AccessToken);
        request.AddHeader("cookie",
            $"Slido.EventAuthTokens=\"{EventUuid},{authResponse.AccessToken}\"");
    }

    private static RestClient GetRestClient(string eventUuid)
    {
        //New HttpClient each time to rest the connection, allowing us to start a new session with slido.
        var client = new HttpClient();
        client.BaseAddress = new Uri($"https://app.sli.do/eu1/api/v0.5/events/{eventUuid}/");
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
        return new RestClient(client);
    }
}