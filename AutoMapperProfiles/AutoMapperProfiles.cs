using AutoMapper;
using POSProject.Backend.DTOModels;
using POSProject.Backend.Models;
using POSProject.Shared;

namespace POSProject.AutoMapperProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Client, ClientInfoResponse>();
        CreateMap<Client, ClientModel>();
        CreateMap<ClientModel, Client>();
        CreateMap<ClientModel, User>();
        CreateMap<User, ClientModel>();
        CreateMap<Offer, OfferResponse>();
    }
}