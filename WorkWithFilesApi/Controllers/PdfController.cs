using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using WorkWithFilesApi.Data;
using WorkWithFilesApi.Helpers;

namespace WorkWithFilesApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IConverter _converter;

        public PdfController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public IActionResult GetPdf()
        {
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "Files", "PdfDocuments", "pdf.pdf");

            var html = PdfGenerator.GetHtmlGames(GamesDb.GetAll());

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings(10, 10, 10, 10),
                DocumentTitle = "Pdf",
      
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                //Page = "https://site.com",
                WebSettings = {
                    DefaultEncoding = "utf-8", 
                    //UserStyleSheet = "path to css file"
                },
                HeaderSettings = { FontName = "Arial", FontSize = 12, Right = "Page [page] to [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 12, Center = "[page]", Line = false },
            };

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var pdfFile = _converter.Convert(pdf);

            return File(pdfFile, "application/pdf"); //opening in browser
            //return File(pdfFile, "application/pdf", "file.pdf"); //downloading
        }
    }
}
