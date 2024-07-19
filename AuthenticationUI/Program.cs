using AuthenticationUI.Models;
using System.Text;
using System.Text.Json;

Console.WriteLine("Enter Username:");
string username = Console.ReadLine();

Console.WriteLine("Enter Password:");
string password = Console.ReadLine();

HttpClient client = new();
var loginRequest = new { Username = username, Password = password };
string jsonContent = JsonSerializer.Serialize(loginRequest);
StringContent content = new(jsonContent, Encoding.UTF8, "application/json");

HttpResponseMessage response = await client.PostAsync("http://localhost:5246/login", content);

if (response.IsSuccessStatusCode)
{
    string responseContent = await response.Content.ReadAsStringAsync();
    UserProfileDTO? userProfile = JsonSerializer.Deserialize<UserProfileDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    Console.WriteLine($"Welcome {userProfile!.FirstName + " " + userProfile.LastName}");
    Console.WriteLine($"Username: {userProfile.Username}");
    Console.WriteLine($"Email: {userProfile.Email}");
    Console.WriteLine($"Token: {userProfile.Token}");
}
else
{
    string errorMessage = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Login failed: {errorMessage}");
}
