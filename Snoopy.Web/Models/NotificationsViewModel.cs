using System;
 
namespace SocialGoal.Web.ViewModels
{
    public class NotificationViewModel
    {
 
        public int NotificationId { get; set; }
        public int NotificationType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
      
        
   
    }
}