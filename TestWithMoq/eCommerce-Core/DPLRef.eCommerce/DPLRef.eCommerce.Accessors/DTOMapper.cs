using AutoMapper;
using DPLRef.eCommerce.Accessors.DataTransferObjects;
using DPLRef.eCommerce.Accessors.EntityFramework;
using DPLRef.eCommerce.Common.Contracts;
using System.Runtime.CompilerServices;
using Cart = DPLRef.eCommerce.Accessors.EntityFramework.Cart;
using CartItem = DPLRef.eCommerce.Accessors.EntityFramework.CartItem;
using Order = DPLRef.eCommerce.Accessors.DataTransferObjects.Order;
using OrderLine = DPLRef.eCommerce.Accessors.EntityFramework.OrderLine;
using Product = DPLRef.eCommerce.Accessors.EntityFramework.Product;
using Seller = DPLRef.eCommerce.Accessors.EntityFramework.Seller;

[assembly: InternalsVisibleTo("DPLRef.eCommerce.Tests.AccessorTests")]
[assembly: InternalsVisibleTo("DPLRef.eCommerce.Tests.IntegrationTests")]

namespace DPLRef.eCommerce.Accessors
{
    //static class DTOMapper
    //{
    //    public static T Map<T>(object obj)
    //    {
    //        var dest = Activator.CreateInstance<T>();
    //        Map(obj, dest);
    //        return dest;
    //    }

    //    public static void Map(object obj, object dest)
    //    {
    //        DTOPropCopy.CopyProps(obj, dest);
    //    }

    //    public static WebStoreCatalog Map(EntityFramework.CatalogExtended c)
    //    {
    //        var result = Map<WebStoreCatalog>(c.Catalog);
    //        result.SellerName = c.SellerName;
    //        return result;
    //    }
    //}

    internal static class DTOMapper
    {
        private static IMapper _mapper;
        private static IConfigurationProvider _config;

        private static IMapper Mapper => _mapper ??= Configuration.CreateMapper();

        public static IConfigurationProvider Configuration
        {
            get
            {
                if (_config == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        _ = cfg.CreateMap<EntityFramework.Catalog, WebStoreCatalog>()
                            .ForMember(a => a.SellerName, b => b.Ignore());

                        _ = cfg.CreateMap<WebStoreCatalog, EntityFramework.Catalog>()
                            .ForMember(a => a.CreatedAt, b => b.Ignore())
                            .ForMember(a => a.UpdatedAt, b => b.Ignore());

                        _ = cfg.CreateMap<Product, DataTransferObjects.Product>();

                        _ = cfg.CreateMap<DataTransferObjects.Product, Product>()
                            .ForMember(a => a.CreatedAt, b => b.Ignore())
                            .ForMember(a => a.UpdatedAt, b => b.Ignore());

                        _ = cfg.CreateMap<Seller, DataTransferObjects.Seller>();
                        _ = cfg.CreateMap<DataTransferObjects.Seller, Seller>()
                            .ForMember(a => a.CreatedAt, b => b.Ignore())
                            .ForMember(a => a.UpdatedAt, b => b.Ignore());

                        //cfg.CreateMap<EntityFramework.Cart, Cart>()
                        //   .ForMember(a => a.BillingAddress, b => b.Ignore())
                        //   .ForMember(a => a.ShippingAddress, b => b.Ignore())
                        //   .ForMember(a => a.CartItems, b => b.Ignore());

                        _ = cfg.CreateMap<OrderLine, DataTransferObjects.OrderLine>();

                        #region Cart

                        _ = cfg.CreateMap<CartItem, DataTransferObjects.CartItem>()
                            .ForMember(a => a.ProductName, b => b.Ignore());

                        _ = cfg.CreateMap<Cart, DataTransferObjects.Cart>()
                            .ForMember(a => a.BillingAddress, b => b.MapFrom(c => new Address
                            {
                                EmailAddress = c.BillingEmailAddress,
                                First = c.BillingFirst,
                                Last = c.BillingLast,
                                Addr1 = c.BillingAddr1,
                                Addr2 = c.BillingAddr2,
                                City = c.BillingCity,
                                Postal = c.BillingPostal,
                                State = c.BillingState
                            }))
                            .ForMember(a => a.ShippingAddress, b => b.MapFrom(c => new Address
                            {
                                EmailAddress = c.ShippingEmailAddress,
                                First = c.ShippingFirst,
                                Last = c.ShippingLast,
                                Addr1 = c.ShippingAddr1,
                                Addr2 = c.ShippingAddr2,
                                City = c.ShippingCity,
                                Postal = c.ShippingPostal,
                                State = c.ShippingState
                            }))
                            .ForMember(a => a.CartItems, b => b.Ignore());

                        _ = cfg.CreateMap<DataTransferObjects.Cart, Cart>()
                            .ForMember(a => a.CatalogId, b => b.Ignore())
                            .ForMember(a => a.CreatedAt, b => b.Ignore())
                            .ForMember(a => a.UpdatedAt, b => b.Ignore())
                            .ForMember(a => a.BillingFirst, b => b.MapFrom(c => c.BillingAddress.First))
                            .ForMember(a => a.BillingLast, b => b.MapFrom(c => c.BillingAddress.Last))
                            .ForMember(a => a.BillingEmailAddress, b => b.MapFrom(c => c.BillingAddress.EmailAddress))
                            .ForMember(a => a.BillingCity, b => b.MapFrom(c => c.BillingAddress.City))
                            .ForMember(a => a.BillingPostal, b => b.MapFrom(c => c.BillingAddress.Postal))
                            .ForMember(a => a.BillingAddr1, b => b.MapFrom(c => c.BillingAddress.Addr1))
                            .ForMember(a => a.BillingAddr2, b => b.MapFrom(c => c.BillingAddress.Addr2))
                            .ForMember(a => a.BillingState, b => b.MapFrom(c => c.BillingAddress.State))
                            .ForMember(a => a.ShippingFirst, b => b.MapFrom(c => c.ShippingAddress.First))
                            .ForMember(a => a.ShippingLast, b => b.MapFrom(c => c.ShippingAddress.Last))
                            .ForMember(a => a.ShippingEmailAddress, b => b.MapFrom(c => c.ShippingAddress.EmailAddress))
                            .ForMember(a => a.ShippingCity, b => b.MapFrom(c => c.ShippingAddress.City))
                            .ForMember(a => a.ShippingPostal, b => b.MapFrom(c => c.ShippingAddress.Postal))
                            .ForMember(a => a.ShippingAddr1, b => b.MapFrom(c => c.ShippingAddress.Addr1))
                            .ForMember(a => a.ShippingAddr2, b => b.MapFrom(c => c.ShippingAddress.Addr2))
                            .ForMember(a => a.ShippingState, b => b.MapFrom(c => c.ShippingAddress.State));

                        #endregion
                    });
                    _config = config;
                }

                return _config;
            }
        }

        public static void Map(object source, object dest) => _ = Mapper.Map(source, dest, source.GetType(), dest.GetType());

        public static T Map<T>(object source) => Mapper.Map<T>(source);

        public static Order MapOrder(EntityFramework.Order model)
        {
            var result = new Order
            {
                Id = model.Id,
                AuthorizationCode = model.AuthorizationCode,
                ShippingProvider = model.ShippingProvider,
                TrackingCode = model.TrackingCode,
                Notes = model.Notes,
                SubTotal = model.SubTotal,
                TaxAmount = model.TaxAmount,
                Total = model.Total,
                Status = model.Status
            };
            result.Total = model.Total;
            result.SellerId = model.SellerId;

            result.BillingAddress = new Address
            {
                First = model.BillingFirst,
                Last = model.BillingLast,
                Addr1 = model.BillingAddr1,
                EmailAddress = model.BillingEmailAddress,
                Addr2 = model.BillingAddr2,
                City = model.BillingCity,
                State = model.BillingState,
                Postal = model.BillingPostal
            };

            result.ShippingAddress = new Address
            {
                First = model.ShippingFirst,
                Last = model.ShippingLast,
                EmailAddress = model.ShippingEmailAddress,
                Addr1 = model.ShippingAddr1,
                Addr2 = model.ShippingAddr2,
                City = model.ShippingCity,
                State = model.ShippingState,
                Postal = model.ShippingPostal
            };

            return result;
        }

        public static void MapOrder(Order order, EntityFramework.Order model)
        {
            // TODO: We need to change the mapping to be more robust and create test errors if we are missing fields to be mapped         
            model.Id = order.Id;
            model.AuthorizationCode = order.AuthorizationCode;
            model.SubTotal = order.SubTotal;
            model.TaxAmount = order.TaxAmount;
            model.Total = order.Total;
            model.Status = order.Status;
        }

        public static void MapBilling(Address address, Cart cart)
        {
            cart.BillingFirst = address.First;
            cart.BillingLast = address.Last;
            cart.BillingEmailAddress = address.EmailAddress;
            cart.BillingAddr1 = address.Addr1;
            cart.BillingAddr2 = address.Addr2;
            cart.BillingCity = address.City;
            cart.BillingState = address.State;
            cart.BillingPostal = address.Postal;
        }

        public static void MapShipping(Address address, Cart cart)
        {
            cart.ShippingFirst = address.First;
            cart.ShippingLast = address.Last;
            cart.ShippingEmailAddress = address.EmailAddress;
            cart.ShippingAddr1 = address.Addr1;
            cart.ShippingAddr2 = address.Addr2;
            cart.ShippingCity = address.City;
            cart.ShippingState = address.State;
            cart.ShippingPostal = address.Postal;
        }

        public static void MapBilling(Address address, EntityFramework.Order order)
        {
            order.BillingFirst = address.First;
            order.BillingLast = address.Last;
            order.BillingEmailAddress = address.EmailAddress;
            order.BillingAddr1 = address.Addr1;
            order.BillingAddr2 = address.Addr2;
            order.BillingCity = address.City;
            order.BillingState = address.State;
            order.BillingPostal = address.Postal;
        }

        public static void MapShipping(Address address, EntityFramework.Order order)
        {
            order.ShippingFirst = address.First;
            order.ShippingLast = address.Last;
            order.ShippingEmailAddress = address.EmailAddress;
            order.ShippingAddr1 = address.Addr1;
            order.ShippingAddr2 = address.Addr2;
            order.ShippingCity = address.City;
            order.ShippingState = address.State;
            order.ShippingPostal = address.Postal;
        }

        public static WebStoreCatalog Map(CatalogExtended c)
        {
            var result = Map<WebStoreCatalog>(c.Catalog);
            result.SellerName = c.SellerName;
            return result;
        }
    }
}