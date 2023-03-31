namespace MyTeamsApp.Models
{
    public class HelloWorldModel
    {
        private static string receivedResponse = string.Empty;
        private static string[] sampleResponse = {
            "Jeff Teper is a technology",
            "executive",
            "whocurrently serves",
            "as the Corporate Vice",
            "President for",
            "Microsoft Teams, OneDrive, SharePoint, and Yammer at Microsoft",
            "Corporation. He is ",
            "considered to be one of ",
            "the key architects of",
            "Microsoft SharePoint,",
            "which",
            "is a web-based collaborative platform that allows organizations",
            "to",
            "manage",
            "and share content",
            ", knowledge, and applications",
            ".\r\n\r\nTeper has been with Microsoft since 1991",
            "and has held several leadership",
            "roles within the company,",
            "including",
            "serving as the ",
            "Corporate Vice President of the Office Server Group, the",
            "General Manager",
            "of Business Portal, and the Group",
            "Program Manager for the Internet Explorer team.\r\n\r\nTeper",
            "holds a Bachelor of Science",
            "degree",
            "in computer science from the University of",
            "Illinois at Urbana-Champaign",
            "and a Master of Business Administration",
            "degree from Harvard Business",
            "School., He is widely recognized as a technology thought leader",
            "and has been named to several lists of the most influential people in the technology industry." };
        public string Title { get; set; }

        public string Body { get; set; }

        internal static string GetSampleCard(int streamId)
        {
            receivedResponse += sampleResponse[streamId] + " ";
            return @$"{{
                        ""type"":""AdaptiveCard"",
                        ""body"":[
                            {{
                                ""type"":""TextBlock"",
                                ""size"":""Medium"",
                                ""weight"":""Bolder"",
                                ""text"":""This is the stream chunk#{streamId}""
                            }},
                            {{
                                ""type"":""TextBlock"",
                                ""text"":""{receivedResponse}"",
                                ""wrap"":true
                            }}
                        ],
                        ""actions"":[
                            {{
                                ""type"":""Action.OpenUrl"",
                                ""title"":""Bot Framework Docs"",
                                ""url"":""https://docs.microsoft.com/en-us/azure/bot-service/?view=azure-bot-service-4.0""
                            }},
                            {{
                                ""type"":""Action.OpenUrl"",
                                ""title"":""Teams Toolkit Docs"",
                                ""url"":""https://aka.ms/teamsfx-docs""
                            }}
                        ],
                        ""$schema"":""http://adaptivecards.io/schemas/adaptive-card.json"",
                        ""version"":""1.4""
                    }}";
        }
    }
}
