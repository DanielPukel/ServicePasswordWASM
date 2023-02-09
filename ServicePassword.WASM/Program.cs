using AuthentificationTEsting.WASP;
using AuthentificationTEsting.WASP.Http;
using AuthentificationTEsting.WASP.Query;
using AuthentificationTEsting.WASP.Stores;
using Client;
using Firebase.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var baseAddress = builder.Configuration.GetValue<string>("SERVICE_PASSWORD_API_BASE_URL");
builder.Services.AddSingleton(new HttpClient
{
    BaseAddress = new Uri(baseAddress!)
}
);

builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("FIREBASE_API_KEY"));
var firebaseApiKey = builder.Configuration["FIREBASE_API_KEY"];

//builder.Services.AddSingleton<FirebaseAuthProvider>();

builder.Services.AddSingleton<FirebaseAuthProvider>(
 new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey)));

builder.Services.AddTransient<FirebaseAuthHttpMessageHandler>();

builder.Services.AddRefitClient<IGetServicePasswordQuery>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress!)).AddHttpMessageHandler<FirebaseAuthHttpMessageHandler>();

builder.Services.AddSingleton<AuthentificationStore>();

//Add sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


await builder.Build().RunAsync();
