using AuthentificationTEsting.WASP.Stores;
using Firebase.Auth;
using System.Net.Http.Headers;

namespace AuthentificationTEsting.WASP.Http
{
    public class FirebaseAuthHttpMessageHandler : DelegatingHandler
    {
        private readonly AuthentificationStore _authentificationStore;

        public FirebaseAuthHttpMessageHandler(AuthentificationStore authentificationStore)
        {
            _authentificationStore = authentificationStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        { 

            FirebaseAuthLink firebaseAuthLink = await _authentificationStore.GetFreshAuthAsync();


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", firebaseAuthLink.FirebaseToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
