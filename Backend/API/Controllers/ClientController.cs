using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSProject.Backend.DTOModels;
using POSProject.Backend.Models;
using POSProject.Backend.Services.Interfaces;

namespace POSProject.Backend.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize(Roles = "Client")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IDataService<Client> clientService;
    private readonly IDataService<Offer> offerService;
    private readonly IMapper mapper;

    public ClientController(IHttpContextAccessor httpContextAccessor, IDataService<Client> clientService,
        IDataService<Offer> offerService, IMapper mapper)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.clientService = clientService;
        this.offerService = offerService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Returns info about current client.
    /// </summary>
    /// <returns>Client Info about current authorized user</returns>
    /// <response code="200">Returns info</response>
    /// <response code="400">If the item is null</response>
    [HttpGet]
    [Route("get_info")]
    public async Task<IActionResult> GetClientInfo()
    {
        var clientInfo = await clientService.GetFirstOrDefaultByQueryAsync(x =>
            x.Email == httpContextAccessor.HttpContext.User.Identity.Name);
        if (clientInfo == null) return BadRequest();
        var clientInfoResponse = mapper.Map<ClientInfoResponse>(clientInfo);
        return Ok(new {clientInfoResponse});
    }

    /// <summary>
    /// Returns all offers for current client.
    /// </summary>
    /// <returns>Offers for current client</returns>
    /// <response code="200">Returns list of offers</response>
    [HttpGet]
    [Route("get_offers")]
    public async Task<IActionResult> GetOffers()
    {
        var offers = (await offerService.GetAllAsync()).Select(mapper.Map<OfferResponse>);
        return Ok(new {offers});
    }
}