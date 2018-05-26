using API_intership.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



namespace API_intership.Controllers
{
    class ApiController
    {
        private const string name = "A";

        private const string password = "VmbS64EPwy";

        HttpClient client = new HttpClient();
        const string uriAddress = "http://51.144.236.66/";
        SkillDTO[] skills;
        DetailsFullDTO detailsFull;
        string userID= "2fea92f8-c8d5-4f87-8cce-68c0b874b3fc";

        public void AddNewUser()
        {
            try
            {            
                UserNewDTO userNewDTO = new UserNewDTO("porebski.alex@gmail.com", "A");


                string content = JsonConvert.SerializeObject(userNewDTO);
                Console.WriteLine(content);
                Console.WriteLine("Sending...");
                AddNewserAsync(userNewDTO).GetAwaiter().GetResult();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }

         async Task AddNewserAsync(UserNewDTO user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response= await client.PostAsync("users", content);

            Console.WriteLine("Recived!");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);
            string s = await response.Content.ReadAsStringAsync();
            File.WriteAllText("userCreated.txt", s);           
            Console.WriteLine(s);
            client.CancelPendingRequests();
        }

        public void GetCreatedUser()
        {          
             GetUserCreatedAsync().GetAwaiter().GetResult();            
        }

        async Task GetUserCreatedAsync()
        {
            var response = await client.GetAsync("users/"+userID);
            string tmp = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response.StatusCode);
            try
            {
                UserCreatedDTO userRecived = JsonConvert.DeserializeObject<UserCreatedDTO>(tmp);
                Console.WriteLine(userRecived.password + "\n" + userRecived.id + "\n" + userRecived.email);
            }
            catch (Exception e) { }         
            client.CancelPendingRequests();
        }

       

        public void GetSkills()
        {
            GetSkillsAsync().GetAwaiter().GetResult();
        }

        async Task GetSkillsAsync()
        {          
            var response = await client.GetAsync("skills");
            Console.WriteLine(response.StatusCode);
            string tmp = await response.Content.ReadAsStringAsync();
            try
            {
                skills = JsonConvert.DeserializeObject<SkillDTO[]>(tmp);
            }catch(Exception e){}

            if(skills!=null)
                foreach (SkillDTO s in skills)
                {
                    Console.WriteLine(s.Id + " " + s.SkillName + "\n");
                }
            client.CancelPendingRequests();
        }

        public void AddSkillToUser()
        {
            List<Int64> list = new List<Int64>();
            int input = 0;
            string tym;
            Console.WriteLine("Add skill to list:");
            do
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    if (skills != null && skills.Length >= input && input != 0)
                    {
                        list.Add(input);
                    }
                }
                catch (Exception e)
                {
                    input = 0;
                }

            } while (input != 0);

            SaveSkillsRequestDTO saveSkills = new SaveSkillsRequestDTO();
            saveSkills.userId = userID;
            saveSkills.skillsIds = list.ToArray<Int64>();
            AddSkillsToUserAsync(saveSkills).GetAwaiter();
        }

        async Task AddSkillsToUserAsync(SaveSkillsRequestDTO saveSkills)
        {
            var content = new StringContent(JsonConvert.SerializeObject(saveSkills), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("users/skills/", content);
            Console.WriteLine(response.StatusCode);
        }


        public void AddDetailsToUser()
        {
            DetailsNewDTO detailsNew = new DetailsNewDTO();
            detailsNew.fieldOfStudy = "Informatics";
            detailsNew.firstName = "Alex";
            detailsNew.lastName = "Porebski";
            detailsNew.university = "Politechnika Slaska";
            detailsNew.yearOfStudy = 3;
            AddDetailsToUserAsync(detailsNew).GetAwaiter();

        }
        async Task AddDetailsToUserAsync(DetailsNewDTO detailsNew)
        {
            var content = new StringContent(JsonConvert.SerializeObject(detailsNew), Encoding.UTF8, "application/json");
            var tmp = await content.ReadAsStringAsync();
            Console.WriteLine(tmp);
            var response = await client.PutAsync("users/details/", content);
            Console.WriteLine(response.StatusCode);
        }



        public void GetAllUserDetails()
        {
            GetAllUserDetailsAsync().GetAwaiter();
            if (detailsFull != null)
            {
                Console.WriteLine("FieldOfStudy: " + detailsFull.fieldOfStudy+"\n");
                Console.WriteLine("FirstName: " + detailsFull.firstName + "\n");
                Console.WriteLine("LastName: " + detailsFull.lastName + "\n");
                Console.WriteLine("ID: " + detailsFull.id + "\n");
                Console.WriteLine("Email: " + detailsFull.user.email + "\n");
                Console.WriteLine("YearsOfStudy: " + detailsFull.yearOfStudy + "\n");
                foreach (SkillDTO skill in detailsFull.user.skills)
                {
                    Console.WriteLine("  "+skill.Id+skill.SkillName  + "\n");
                }
            }
        }

        async Task GetAllUserDetailsAsync()
        {
            var response = await client.GetAsync("users/alldetails/"+userID);
            string tmp = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response.StatusCode);
            try
            {
                Console.WriteLine(tmp+"\n");
                detailsFull = JsonConvert.DeserializeObject<DetailsFullDTO>(tmp);
                if (detailsFull != null)
                {
                    Console.WriteLine("ID: " + detailsFull.id + "\n");
                    Console.WriteLine("First Name: " + detailsFull.firstName + "\n");
                    Console.WriteLine("Last Name: " + detailsFull.lastName + "\n");
                    Console.WriteLine("Field Of Study: " + detailsFull.fieldOfStudy + "\n");
                    Console.WriteLine("University: " + detailsFull.university + "\n");
                    Console.WriteLine("Years Of Study: " + detailsFull.yearOfStudy + "\n");
                    Console.WriteLine(" User ID: " + detailsFull.user.id + "\n");
                    Console.WriteLine(" Email: " + detailsFull.user.email + "\n");
                    Console.WriteLine(" User Name: " + detailsFull.user.name + "\n");
                    foreach (SkillDTO skill in detailsFull.user.skills)
                    {
                        Console.WriteLine("   " + skill.Id +". "+ skill.SkillName + "\n");
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("Błąd odczytu\n");
            }
            client.CancelPendingRequests();
        }

        public void AddSkill()
        {
            Console.WriteLine("Write skill name:");
            string name = Console.ReadLine();
            SkillNewDTO skill = new SkillNewDTO();
            skill.skillName = name;
            AddSkillAsync(skill).GetAwaiter();
        }

        async Task AddSkillAsync(SkillNewDTO skill)
        {
            var content = new StringContent(JsonConvert.SerializeObject(skill), Encoding.UTF8, "application/json");
            var tmp = await content.ReadAsStringAsync();
            Console.WriteLine(tmp);
            var response = await client.PostAsync("skills", content);
            Console.WriteLine(response.StatusCode);
        }

        public void Login()
        {
            LoginAsync().GetAwaiter();
        }
        async Task LoginAsync()
        {

        }

        public void ConfigureHttpClient()
        {
            client.BaseAddress = new Uri(uriAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                                       "Basic",
                                        Convert.ToBase64String(
                                                   System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                   string.Format("{0}:{1}", name, password))));
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
