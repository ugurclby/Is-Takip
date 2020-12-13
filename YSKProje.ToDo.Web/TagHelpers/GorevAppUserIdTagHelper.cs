using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;

namespace YSKProje.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("getirAppUserId")]
    public class GorevAppUserIdTagHelper : TagHelper
    {
        private readonly IGorevService _gorevService;
        public GorevAppUserIdTagHelper(IGorevService gorevService)
        {
            _gorevService = gorevService;
        }
        public int AppUserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var gorevAppUser =  _gorevService.GetirileAppUserId(AppUserId);
            var tamamlananGorevSayisi = gorevAppUser.Where(x => x.Durum).Count();
            var tamamlanmayanGorevSayisi = gorevAppUser.Where(x => !x.Durum).Count();

            string sonuc = $"<strong>Tamamlanan Görev Sayısı: {tamamlananGorevSayisi}</strong> </br> <strong>Tamamlanmayan Görev Sayısı: {tamamlanmayanGorevSayisi}</strong>";

            output.Content.SetHtmlContent(sonuc);
        }
    }
}
