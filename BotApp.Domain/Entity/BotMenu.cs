
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tBotMenu")]
    public class BotMenu
    {
        [Key]
        public int BotMenuId { get; set; }
        public int? ParentBotMenuId { get; set; }
        public int OrderId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string SubTitle { get; set; }
        public string CreatedBy { get; set; }
        public string BaseUrl { get; set; }
        public string PreRenderShowOnlyChildrenHook { get; set; }
        public string ThirdPartySystemReference { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? IsDeleted { get; set; }
    } 
}
