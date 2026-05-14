using DuelRecords.Api.Services.YgoServices;
using Microsoft.AspNetCore.Mvc;

namespace DuelRecords.Api.Controllers;

[ApiController]
[Route("api/ygo/admin")]
public class YgoAdminController : ControllerBase
{
    private readonly YgoImportService _importService;

    public YgoAdminController(YgoImportService importService)
    {
        _importService = importService;
    }

    [HttpPost("importar-json")]
    public async Task<IActionResult> ImportarJson()
    {
        //var filePath = "/mnt/user/appdata/duelrecords/backups/ygo/cards.json";
        var filePath = @"E:\Projetos\DuelRecords\cards.json";

        var total = await _importService.ImportFromJsonFileAsync(filePath);

        return Ok(new
        {
            Mensagem = "Importação concluída com sucesso.",
            TotalProcessado = total
        });
    }
}