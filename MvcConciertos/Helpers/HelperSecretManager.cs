using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace MvcConciertos.Helpers
{
    public static class HelperSecretManager
    {
        public static async Task<string> GetSecret(string secretName)
        {
            string region = "us-east-1";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }

            return response.SecretString;
        }
    }
}
