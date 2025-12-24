namespace Project.API.Helpers
{
    public class EmailBodyBuilder
    {
        private readonly IWebHostEnvironment _environment;

        public EmailBodyBuilder(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GenerateEmailBody(string templateName, Dictionary<string, string> templateModel)
        {
            var templatePath = Path.Combine(
                _environment.ContentRootPath,
                "EmailTemplates",
                $"{templateName}.html"
            );

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found: {templatePath}");
            }

            var template = File.ReadAllText(templatePath);

            foreach (var placeholder in templateModel)
            {
                template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            return template;
        }


    }
}