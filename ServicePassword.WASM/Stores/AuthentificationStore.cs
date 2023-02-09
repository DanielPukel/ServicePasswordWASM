

using Firebase.Auth;

namespace AuthentificationTEsting.WASP.Stores
{
    public class AuthentificationStore
    {
        private readonly FirebaseAuthProvider _firebaseAuthProvider;


        private FirebaseAuthLink? _currentFirebaseAuthLink;

        public AuthentificationStore(FirebaseAuthProvider firebaseAuthProvider)
        {
            _firebaseAuthProvider = firebaseAuthProvider;
        }
        public User? currentUser => _currentFirebaseAuthLink?.User;

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                _currentFirebaseAuthLink = await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);
                if (_currentFirebaseAuthLink != null)
                {
                    Console.WriteLine(_currentFirebaseAuthLink.FirebaseToken);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task Verify()
        {
            if (_currentFirebaseAuthLink != null)
            {
                await _firebaseAuthProvider.SendEmailVerificationAsync(_currentFirebaseAuthLink.FirebaseToken);
            }
        }

        internal Task<FirebaseAuthLink> GetFreshAuthAsync()
        {
            return _currentFirebaseAuthLink.GetFreshAuthAsync();
        }
    }
}
