using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SmartBazaar.Data.Entities;
using SmartBazaar.Data;

namespace SmartBazaar.Web.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMaps()
        {
            #region Admin Product Tax

            Mapper.CreateMap<Tax_Products, Areas.Admin.Models.ProductTaxListViewModel>();

            Mapper.CreateMap<Tax_Products, Areas.Admin.Models.ProductTaxEditViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.ProductTaxEditViewModel, Tax_Products>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );

            #endregion

            #region Admin Catalog Brand

            Mapper.CreateMap<Catalog_Brands, Areas.Admin.Models.CatalogBrandListViewModel>();

            Mapper.CreateMap<Catalog_Brands, Areas.Admin.Models.CatalogBrandEditViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.CatalogBrandEditViewModel, Catalog_Brands>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );

            Mapper.CreateMap<Catalog_Brands_Lang, Areas.Admin.Models.CatalogBrandLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Catalog_Brands, Areas.Admin.Models.CatalogBrandLangViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                    dest => dest.RefId,
                    act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Id,
                    act => act.Ignore()
                )
                ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogBrandLangViewModel, Catalog_Brands_Lang>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Admin Catalog Category

            Mapper.CreateMap<Catalog_Categories, Areas.Admin.Models.CatalogCategoryListViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.Parent,
                act => act.MapFrom(src => src.Catalog_Categories_Parent.Title)
                );

            Mapper.CreateMap<Catalog_Categories, Areas.Admin.Models.CatalogCategoryEditViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.ParentTitle,
                act => act.MapFrom(src => src.Catalog_Categories_Parent.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.CatalogCategoryEditViewModel, Catalog_Categories>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );

            Mapper.CreateMap<Catalog_Categories, Models.Site.CatalogCategoryDetialViewModel>();

            Mapper.CreateMap<Catalog_Categories_Lang, Areas.Admin.Models.CatalogCategoryLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Catalog_Categories, Areas.Admin.Models.CatalogCategoryLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.Description,
                act => act.MapFrom(src => src.Description)
                )
                .ForMember(
                dest => dest.RefId,
                act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                )
                ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogCategoryLangViewModel, Catalog_Categories_Lang>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Acmin Catalog Products

            Mapper.CreateMap<Catalog_Products_Properties, Areas.Admin.Models.CatalogProductPropertiesEditViewModel>();

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductPropertiesEditViewModel, Catalog_Products_Properties>()
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );


            Mapper.CreateMap<Catalog_Product_Images, Areas.Admin.Models.CatalogProductImagesEditViewModel>();

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductImagesEditViewModel, Catalog_Product_Images>()
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );


            Mapper.CreateMap<Catalog_Products, Areas.Admin.Models.CatalogProductListViewModel>()
                .ForMember(
                dest => dest.BrandTitle,
                act => act.MapFrom(src => src.Catalog_Brands.Title)
                )
                .ForMember(
                dest => dest.CategoryTitle,
                act => act.MapFrom(src => src.Catalog_Categories.Title)
                );

            Mapper.CreateMap<Catalog_Products, Areas.Admin.Models.CatalogProductEditViewModel>()
                .ForMember(
                dest => dest.FakeCategoryId,
                act => act.MapFrom(src => src.Catalog_Categories.Title)
                )
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.Properties,
                act => act.MapFrom(src =>
                    Mapper.Map<Catalog_Products_Properties[], List<Areas.Admin.Models.CatalogProductPropertiesEditViewModel>>(src.Catalog_Products_Properties.ToArray())
                    )
                )
                .ForMember(
                dest => dest.Images,
                act => act.MapFrom(src =>
                    Mapper.Map<Catalog_Product_Images[], List<Areas.Admin.Models.CatalogProductImagesEditViewModel>>
                        (src.Catalog_Product_Images.ToArray())
                    )
                )
                .ForMember(
                    dest => dest.Relations,
                    act => act.MapFrom(src => 
                        Mapper.Map<Catalog_Products_Relations[], List<Areas.Admin.Models.CatalogProductsRelationsEditViewModel>>(
                            src.Related_Catalog_Products_Relations.ToArray()
                        )
                    )
                )
                ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductEditViewModel, Catalog_Products>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            Mapper.CreateMap<Catalog_Products_Properties_Lang, Areas.Admin.Models.CatalogProductPropertiesLangViewModel>();

            Mapper.CreateMap<Catalog_Products_Properties, Areas.Admin.Models.CatalogProductPropertiesLangViewModel>()
                .ForMember(
                dest => dest.RefId,
                act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductPropertiesLangViewModel, Catalog_Products_Properties_Lang>();

            Mapper.CreateMap<Catalog_Products_Lang, Areas.Admin.Models.CatalogProductLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Catalog_Products, Areas.Admin.Models.CatalogProductLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.RefId,
                act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                dest => dest.Properties,
                act => act.MapFrom(src => Mapper.Map<List<Areas.Admin.Models.CatalogProductPropertiesLangViewModel>>(src.Catalog_Products_Properties.ToArray()))
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                )
                ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductLangViewModel, Catalog_Products_Lang>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Admin Catalog Campaign

            Mapper.CreateMap<Catalog_Campaigns_Sources, Areas.Admin.Models.CatalogCampaignsSourcesViewModel>()
                .ForMember(
                dest => dest.ProductName,
                act => act.MapFrom(src => src.Catalog_Products.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.CatalogCampaignsSourcesViewModel, Catalog_Campaigns_Sources>();


            Mapper.CreateMap<Catalog_Campaigns_Destinations, Areas.Admin.Models.CatalogCampaignsDestinationsViewModel>()
                .ForMember(
                dest => dest.ProductName,
                act => act.MapFrom(src => src.Catalog_Products.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.CatalogCampaignsDestinationsViewModel, Catalog_Campaigns_Destinations>();



            Mapper.CreateMap<Catalog_Campaigns, Areas.Admin.Models.CatalogCampaignsListViewModel>()
                .ForMember(
                dest => dest.CampaignMethod,
                act => act.MapFrom(src => Models.Common.CatalogCampaignsListProvider.GetCapaignMethodList()[src.CampaignMethod])
                )
                ;

            Mapper.CreateMap<Catalog_Campaigns, Areas.Admin.Models.CatalogCampaignsEditViewModel>()
                .ForMember(
                dest => dest.Sources,
                act => act.MapFrom(src => Mapper.Map<Catalog_Campaigns_Sources[], List<Areas.Admin.Models.CatalogCampaignsSourcesViewModel>>(src.Catalog_Campaigns_Sources.ToArray()))
                )
                .ForMember(
                dest => dest.Destinations,
                act => act.MapFrom(src => Mapper.Map<Catalog_Campaigns_Destinations[], List<Areas.Admin.Models.CatalogCampaignsDestinationsViewModel>>(src.Catalog_Campaigns_Destinations.ToArray()))
                )
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.SlideUrl,
                act => act.MapFrom(src => src.SliderUrl)
                )
                ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogCampaignsEditViewModel, Catalog_Campaigns>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                )
                .ForMember(
                    dest => dest.SliderUrl,
                    act => act.MapFrom(src => src.SlideUrl)
                )
                ;

            #endregion

            #region Admin Catalog Comments

            Mapper.CreateMap<Catalog_Product_Comments, Areas.Admin.Models.CatalogCommentsListViewModel>()
                .ForMember(dest => dest.ProductTitle, act => act.MapFrom(src => src.Catalog_Products.Title))
                ;

            Mapper.CreateMap<Catalog_Product_Comments, Areas.Admin.Models.CatalogCommentsDetailViewModel>()
                .ForMember(dest => dest.ProductTitle, act => act.MapFrom(src => src.Catalog_Products.Title))
                ;

            #endregion

            #region Admin Catalog Products Relations

            Mapper.CreateMap<Catalog_Products_Relations, Areas.Admin.Models.CatalogProductsRelationsEditViewModel>()
                .ForMember(dest => dest.ProductTitle, act => act.MapFrom(src => src.Catalog_Products.Title))
            ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductsRelationsEditViewModel, Areas.Admin.Models.CatalogProductsRelationsJSONModel>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.ProductTitle))
            ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductsRelationsEditViewModel, Catalog_Products_Relations>();

            #endregion

            #region Admin Customer Groups

            Mapper.CreateMap<Customer_Groups, Areas.Admin.Models.CustomerGroupListViewModel>()
                .ForMember(
                dest => dest.PriceIndex,
                act => act.MapFrom(src => src.PriceIndex.ToString() + ". Fiyat")
                );

            Mapper.CreateMap<Customer_Groups, Areas.Admin.Models.CustomerGroupEditViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.CustomerGroupEditViewModel, Customer_Groups>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Admin Customer Entities

            Mapper.CreateMap<Customer_Entities, Areas.Admin.Models.CustomerEntityListViewModel>()
                .ForMember(
                dest => dest.GroupTitle,
                act => act.MapFrom(src => src.Customer_Groups.Title)
                );

            Mapper.CreateMap<Customer_Entities, Areas.Admin.Models.CustomerEntityEditViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.CustomerEntityEditViewModel, Customer_Entities>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            Mapper.CreateMap<Customer_Entities, Areas.Admin.Models.CustomerEntityDetailViewModel>()
                .ForMember(
                    dest => dest.CustomerTypeTitle,
                    act => act.MapFrom(src => Models.Common.CustomerEntityListProvider.GetCustomerTypeList()[src.CustomerType])
                )
                .ForMember(
                    dest => dest.GenderTitle,
                    act => act.MapFrom(src => src.Gender.HasValue ? Models.Internal.GenderListProvider.GenderTitles()[src.Gender.Value] : "")
                )
                .ForMember(
                    dest => dest.GroupTitle,
                    act => act.MapFrom(src => src.GroupId.HasValue ? src.Customer_Groups.Title : "")
                )
                .ForMember(
                    dest => dest.PriceIndex,
                    act => act.MapFrom(src => src.GroupId.HasValue ? src.Customer_Groups.PriceIndex : 1)
                )
                .ForMember(
                    dest => dest.Addresses,
                    act => act.MapFrom(src => Mapper.Map<Customer_Addresses[], List<Areas.Admin.Models.CustomerAddressEditViewModel>>(src.Customer_Addresses.ToArray()))
                )
                ;

            #endregion

            #region Admin Customer Address

            Mapper.CreateMap<Customer_Addresses, Areas.Admin.Models.CustomerAddressListViewModel>();

            Mapper.CreateMap<Customer_Addresses, Areas.Admin.Models.CustomerAddressEditViewModel>();

            Mapper.CreateMap<Areas.Admin.Models.CustomerAddressEditViewModel, Customer_Addresses>()
                .ForMember(
                    dest => dest.Id,
                    act => act.Ignore()
                );

            #endregion

            #region Admin Shipment Types

            Mapper.CreateMap<Shipment_Types, Areas.Admin.Models.ShipmentTypesListViewModel>()
                .ForMember(
                dest => dest.PricingMethod,
                act => act.MapFrom(src => Models.Common.ShipmentListProvider.GetMethodList()[src.PricingMethod])
                );

            Mapper.CreateMap<Shipment_Types, Areas.Admin.Models.ShipmentTypesEditViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.ShipmentTypesEditViewModel, Shipment_Types>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            Mapper.CreateMap<Shipment_Types_Lang, Areas.Admin.Models.ShipmentTypesLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Shipment_Types, Areas.Admin.Models.ShipmentTypesLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.RefId,
                act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );

            Mapper.CreateMap<Areas.Admin.Models.ShipmentTypesLangViewModel, Shipment_Types_Lang>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Admin Payment Types

            Mapper.CreateMap<Payment_Installment, Areas.Admin.Models.PaymentInstallmentsEditViewModel>();

            Mapper.CreateMap<Areas.Admin.Models.PaymentInstallmentsEditViewModel, Payment_Installment>();

            Mapper.CreateMap<Payment_Types, Areas.Admin.Models.PaymentTypesListViewModel>()
                .ForMember(
                dest => dest.Method,
                act => act.MapFrom(src => Models.Common.PaymentListProvider.GetMethods()[src.Method])
                );

            Mapper.CreateMap<Payment_Types, Areas.Admin.Models.PaymentTypesEditViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.PaymentInstallments,
                act => act.MapFrom(src => Mapper.Map<Payment_Installment[], List<Areas.Admin.Models.PaymentInstallmentsEditViewModel>>(src.Payment_Installment.ToArray()))
                );

            Mapper.CreateMap<Areas.Admin.Models.PaymentTypesEditViewModel, Payment_Types>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            Mapper.CreateMap<Payment_Types_Lang, Areas.Admin.Models.PaymentTypesLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Payment_Types, Areas.Admin.Models.PaymentTypesLangViewModel>()
                .ForMember(
                dest => dest.Caption,
                act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                dest => dest.RefId,
                act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                dest => dest.Id,
                act => act.Ignore()
                );

            Mapper.CreateMap<Areas.Admin.Models.PaymentTypesLangViewModel, Payment_Types_Lang>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Admin Payment Entities

            Mapper.CreateMap<Payment_Entities, Areas.Admin.Models.PaymentEntitiesListViewModel>()
                .ForMember(
                dest => dest.PaymentType,
                act => act.MapFrom(src => src.Payment_Types.Title)
                )
                .ForMember(
                dest => dest.PaymentInstallment,
                act => act.MapFrom(src => src.Payment_Installment.NumberOf.ToString() + " Taksit")
                )
                ;

            Mapper.CreateMap<Payment_Entities, Areas.Admin.Models.PaymentEntitiesEditViewModel>()
                .ForMember(
                dest => dest.PaymentType,
                act => act.MapFrom(src => src.Payment_Types.Title)
                )
                .ForMember(
                dest => dest.PaymentInstallment,
                act => act.MapFrom(src => src.Payment_Installment.NumberOf.ToString() + " Taksit")
                );

            #endregion

            #region Admin Orders

            Mapper.CreateMap<Order_Lines, Areas.Admin.Models.OrderLinesListViewModel>()
                .ForMember(
                    dest => dest.Campaign,
                    act => act.MapFrom(src => src.Catalog_Campaigns.Title)
                )
                .ForMember(
                    dest => dest.ProductCode,
                    act => act.MapFrom(src => src.Catalog_Products.ProductCode)
                )
                .ForMember(
                    dest => dest.ProductName,
                    act => act.MapFrom(src => src.Catalog_Products.Title)
                )
                .ForMember(
                    dest => dest.TaxCost,
                    act => act.Ignore()
                );


            Mapper.CreateMap<Order_Heads, Areas.Admin.Models.OrderHeadsListViewModel>()
                .ForMember(
                dest => dest.CustomerName,
                act => act.MapFrom(src => src.Customer_Entities.Title)
                )
                .ForMember(
                dest => dest.StatusTitle,
                act => act.MapFrom(src => Models.Common.OrderHeadsListProvider.GetStatuses().FirstOrDefault(f => f.Key == src.Status).Value)
                )
                ;

            Mapper.CreateMap<Order_Heads, Areas.Admin.Models.OrderHeadsEditViewModel>()
                .ForMember(
                    dest => dest.CustomerName,
                    act => act.MapFrom(src => src.Customer_Entities.Title)
                )
                .ForMember(
                    dest => dest.ShipmentAddress,
                    act => act.MapFrom(src => Mapper.Map<Areas.Admin.Models.CustomerAddressEditViewModel>(src.Customer_Addresses))
                )
                .ForMember(
                    dest => dest.InvoiceAddress,
                    act => act.MapFrom(src => Mapper.Map<Areas.Admin.Models.CustomerAddressEditViewModel>(src.Customer_Addresses1))
                )
                .ForMember(
                    dest => dest.OrderLines,
                    act => act.MapFrom(src => Mapper.Map<Order_Lines[], List<Areas.Admin.Models.OrderLinesListViewModel>>(src.Order_Lines.ToArray()))
                )
                .ForMember(
                    dest => dest.Payments,
                    act => act.MapFrom(src => Mapper.Map<Payment_Entities[], List<Areas.Admin.Models.PaymentEntitiesListViewModel>>(src.Payment_Entities.ToArray()))
                )
                .ForMember(
                    dest => dest.Customer,
                    act => act.MapFrom(src => Mapper.Map<Areas.Admin.Models.CustomerEntityEditViewModel>(src.Customer_Entities))
                )
                .ForMember(
                    dest => dest.ShipmentTypes,
                    act => act.MapFrom(src => Mapper.Map<Areas.Admin.Models.ShipmentTypesEditViewModel>(src.Shipment_Types))
                )
                ;

            Mapper.CreateMap<Order_Heads, Areas.Admin.Models.NotificViewModel>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Customer_Entities.Title)
                )
                .ForMember(
                    dest => dest.StatusCss,
                    act => act.MapFrom(src => Models.Common.OrderHeadsListProvider.GetStatusCss()[src.Status])
                );
                

            #endregion

            #region Admin Pages

            Mapper.CreateMap<Pages, Areas.Admin.Models.PagesListViewModel>();

            Mapper.CreateMap<Pages, Areas.Admin.Models.PagesEditViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                )
                ;

            Mapper.CreateMap<Areas.Admin.Models.PagesEditViewModel, Pages>()
                .ForMember(
                    dest => dest.Id,
                    act => act.Ignore()
                )
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                )
                ;

            Mapper.CreateMap<Pages_Lang, Areas.Admin.Models.PagesLangViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Pages, Areas.Admin.Models.PagesLangViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                    dest => dest.RefId,
                    act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Id,
                    act => act.Ignore()
                );

            Mapper.CreateMap<Areas.Admin.Models.PagesLangViewModel, Pages_Lang>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Admin Settings

            Mapper.CreateMap<Models.Internal.ContactModel, Areas.Admin.Models.ContactViewModel>();

            Mapper.CreateMap<Areas.Admin.Models.ContactViewModel, Models.Internal.ContactModel>();

            #endregion

            #region Admin Params

            Mapper.CreateMap<Params, Areas.Admin.Models.ParamsListViewModel>();

            Mapper.CreateMap<Params_Groups, Areas.Admin.Models.ParamsGroupsListViewModel>();

            #endregion

            #region Admin HtmlBlocks

            Mapper.CreateMap<HtmlBlocks, Areas.Admin.Models.HtmlBlocksListViewModel>();

            Mapper.CreateMap<HtmlBlocks, Areas.Admin.Models.HtmlBlocksEditViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.HtmlBlocksEditViewModel, HtmlBlocks>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                );

            Mapper.CreateMap<HtmlBlocks, Models.Site.HtmlBlocksViewModel>();

            Mapper.CreateMap<HtmlBlocks_Lang, Areas.Admin.Models.HtmlBlocksLangViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<HtmlBlocks, Areas.Admin.Models.HtmlBlocksLangViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                )
                .ForMember(
                    dest => dest.RefId,
                    act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Id,
                    act => act.Ignore()
                );

            Mapper.CreateMap<Areas.Admin.Models.HtmlBlocksLangViewModel, HtmlBlocks_Lang>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                );

            #endregion

            #region Admin Pos Settins

            Mapper.CreateMap<Pos_Settings, Areas.Admin.Models.PosSettingsListViewModel>();

            Mapper.CreateMap<Pos_Settings, Areas.Admin.Models.PosSettingsDetailViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                )
                .AfterMap((src, dest) =>
                {
                    dest.KPList = KPSerializer.Serializer.Deserialize(src.Data).Select(s => new KPSerializer.KPModel { Key = s.Key, Value = s.Value }).ToList();
                })
                ;

            Mapper.CreateMap<Areas.Admin.Models.PosSettingsDetailViewModel, Pos_Settings>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                )
                .AfterMap((src, dest) =>
                {
                    dest.Data = KPSerializer.Serializer.Serialize(src.KPList.ToDictionary(k => k.Key, v => v.Value));
                })
                ;

            #endregion

            #region Admin Slider

            Mapper.CreateMap<Slider, Areas.Admin.Models.SliderListViewModel>();

            Mapper.CreateMap<Areas.Admin.Models.SliderEditViewModel, Slider>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                );

            Mapper.CreateMap<Slider, Areas.Admin.Models.SliderEditViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                );

            #endregion

            #region Admin Lang

            Mapper.CreateMap<Lang_Books, Areas.Admin.Models.LangBooksListViewModel>();

            Mapper.CreateMap<Lang_Books, Areas.Admin.Models.LangBooksEditViewModel>()
                .ForMember(
                    dest => dest.Caption,
                    act => act.MapFrom(src => src.Title)
                );

            Mapper.CreateMap<Areas.Admin.Models.LangBooksEditViewModel, Lang_Books>()
                .ForMember(
                    dest => dest.Title,
                    act => act.MapFrom(src => src.Caption)
                );

            Mapper.CreateMap<Lang_Dictionary, Areas.Admin.Models.LangDictionaryListViewModel>();

            Mapper.CreateMap<Areas.Admin.Models.LangDictionaryListViewModel, Lang_Dictionary>();

            #endregion

            #region ProductListDisplayModel

            Mapper.CreateMap<Catalog_Products, Models.Site.ProductListDisplayModel>()
                .ForMember(
                    dest => dest.Brand,
                    act => act.MapFrom(src => src.Catalog_Brands.Title)
                )
                .ForMember(
                    dest => dest.Price,
                    act => act.MapFrom(src => Helpers.Contents.CatalogHelper.GetPriceWithDetail(src, 1))
                )
                ;

            #endregion

            #region Customer Account

            Mapper.CreateMap<Models.Site.CustomerRegisterViewModel, Customer_Entities>()
                .ForMember(
                dest => dest.Title,
                act => act.MapFrom(src => src.Caption)
                )
                .BeforeMap((src, dest) => dest.Created = DateTime.Now);

            #endregion

            #region Customer Address

            Mapper.CreateMap<Models.Site.CustomerAddressViewModel, Customer_Addresses>();

            Mapper.CreateMap<Customer_Addresses, Models.Site.CustomerAddressViewModel>();

            #endregion

            #region Product Detail

            Mapper.CreateMap<Catalog_Products_Properties, Models.Site.ProductPropertyViewModel>();

            Mapper.CreateMap<Catalog_Products, Models.Site.ProductPivotsViewModel>()
                .ForMember(
                    dest => dest.Image,
                    act => act.MapFrom(src => src.ImageUrl)
                );

            Mapper.CreateMap<Catalog_Product_Images, Models.Site.ProductDetailImageViewModel>();

            Mapper.CreateMap<Catalog_Products, Models.Site.ProductDetailViewModel>()
                .ForMember(
                    dest => dest.Brand,
                    act => act.MapFrom(src => src.BrandId.HasValue ? src.Catalog_Brands.Title : "")
                )
                .ForMember(
                    dest => dest.Category,
                    act => act.MapFrom(src => src.Catalog_Categories.Title)
                )
                .ForMember(
                    dest => dest.Image,
                    act => act.MapFrom(src => src.ImageUrl)
                )
                .ForMember(
                    dest => dest.Price,
                    act => act.MapFrom(src => Helpers.Contents.CatalogHelper.GetPriceWithDetail(src, 1))
                )
                .ForMember(
                    dest => dest.Detail,
                    act => act.MapFrom(src => src.Description)
                )
                .ForMember(
                    dest => dest.Props,
                    act => act.MapFrom(src => Mapper.Map<Catalog_Products_Properties[], List<Models.Site.ProductPropertyViewModel>>(src.Catalog_Products_Properties.ToArray()))
                )
                .ForMember(
                    dest => dest.Images,
                    act => act.MapFrom(src => Mapper.Map<Catalog_Product_Images[], List<Models.Site.ProductDetailImageViewModel>>(src.Catalog_Product_Images.ToArray())
                    )
                )
                .ForMember(
                    dest => dest.Pivots,
                    act => act.MapFrom(src => 
                        Mapper.Map<Catalog_Products[], List<Models.Site.ProductPivotsViewModel>>(
                            src.Catalog_Campaigns_Sources.Where(w =>
                                w.ProductId == src.Id
                                    && w.Catalog_Campaigns.StartDate <= DateTime.Now
                                    && w.Catalog_Campaigns.EndDate >= DateTime.Now
                            )
                            .SelectMany(m => m.Catalog_Campaigns.Catalog_Campaigns_Destinations)
                            .Select(s => s.Catalog_Products).ToArray()
                        )
                    )
                )
                .ForMember(
                    dest => dest.RPivots,
                    act => act.MapFrom(src =>
                        Mapper.Map<Catalog_Products[], List<Models.Site.ProductPivotsViewModel>>(
                            src.Catalog_Campaigns_Destinations.Where(w =>
                                w.ProductId == src.Id 
                                    && w.Catalog_Campaigns.StartDate <= DateTime.Now
                                    && w.Catalog_Campaigns.EndDate >= DateTime.Now
                            )
                            .SelectMany(m => m.Catalog_Campaigns.Catalog_Campaigns_Sources)
                            .Select(s => s.Catalog_Products).ToArray()
                        )
                    )
                )
                .ForMember(
                    dest => dest.Comments,
                    act => act.MapFrom(src => Mapper.Map<Catalog_Product_Comments[], List<Models.Site.CommentsListViewModel>>(
                            src.Catalog_Product_Comments.Where(w => w.Status == 1).ToArray()
                        ))
                )
                .AfterMap(
                    (src, dest) =>
                    dest.Pivots.ForEach(
                        act =>
                        {
                            act.Price = Helpers.Contents.CatalogHelper.GetRelatedPriceWithDetail(
                                    src.Catalog_Campaigns_Sources.Select(s => s.Catalog_Campaigns)
                                        .SelectMany(m => m.Catalog_Campaigns_Destinations)
                                        .FirstOrDefault(f => f.Catalog_Products.Id == act.Id)
                                        .Catalog_Products
                                , src.Id
                            );
                            act.RelPrice = Helpers.Contents.CatalogHelper.GetPriceWithDetail(src, 1);
                            act.CampaignId = src.Catalog_Campaigns_Sources.Select(s => s.Catalog_Campaigns)
                                            .Where(w => w.Catalog_Campaigns_Destinations.Any(a => a.ProductId == act.Id))
                                            .FirstOrDefault()
                                            .Id;
                            act.Side = 1;
                        }
                    )
                )
                .AfterMap(
                    (src, dest) =>
                    dest.RPivots.ForEach(
                        act =>
                        {
                            act.Price = Helpers.Contents.CatalogHelper.GetPriceWithDetail(
                                src.Catalog_Campaigns_Destinations.Select(s => s.Catalog_Campaigns)
                                    .SelectMany(m => m.Catalog_Campaigns_Sources)
                                    .FirstOrDefault(f => f.Catalog_Products.Id == act.Id)
                                    .Catalog_Products
                                , 1);
                            act.RelPrice = Helpers.Contents.CatalogHelper.GetRelatedPriceWithDetail(
                                src,
                                src.Catalog_Campaigns_Destinations.Select(s => s.Catalog_Campaigns)
                                    .SelectMany(m => m.Catalog_Campaigns_Sources)
                                    .FirstOrDefault(f => f.Catalog_Products.Id == act.Id)
                                    .Catalog_Products.Id
                            );
                            act.CampaignId = src.Catalog_Campaigns_Destinations.Select(s => s.Catalog_Campaigns)
                                            .Where(w => w.Catalog_Campaigns_Sources.Any(a => a.ProductId == act.Id))
                                            .FirstOrDefault()
                                            .Id;
                            act.Side = 2;
                        }
                    )
                )
                .ForMember(
                    dest => dest.Relations,
                    act => act.MapFrom(
                            src => Mapper.Map<Catalog_Products[], List<Models.Site.ProductListDisplayModel>>(src.Related_Catalog_Products.ToArray())
                        )
                )
                .ForMember(
                    dest => dest.SameProducts,
                    act => act.MapFrom(
                        src => Mapper.Map<Catalog_Products[], List<Models.Site.ProductListDisplayModel>>(src.Catalog_Categories.Catalog_Products.ToArray())    
                    )
                )
                ;

            Mapper.CreateMap<Catalog_Product_Comments, Models.Site.CommentsListViewModel>();

            #endregion

            #region Campaigns

            Mapper.CreateMap<Catalog_Campaigns, Models.Site.CampaignSliderViewModel>();

            Mapper.CreateMap<Catalog_Campaigns, Models.Site.CampaignDetailViewModel>()
                .ForMember(
                    dest => dest.SlideUrl,
                    act => act.MapFrom(src => src.SliderUrl)
                );

            #endregion

            #region Payments

            Mapper.CreateMap<Payment_Installment, Models.Site.PaymentInstallmentViewModel>();

            Mapper.CreateMap<Payment_Types, Models.Site.PaymentTypeViewModel>()
                .ForMember(
                    dest => dest.Installments,
                    act => act.MapFrom(src => Mapper.Map<Payment_Installment[], List<Models.Site.PaymentInstallmentViewModel>>(src.Payment_Installment.ToArray()))
                );

            Mapper.CreateMap<Payment_Entities, Models.Site.PaymentEntityViewModel>();
            Mapper.CreateMap<Models.Site.PaymentEntityViewModel, Payment_Entities>();

            Mapper.CreateMap<Models.Site.OrderHeaderProcessModel, Models.Site.PaymentEntityViewModel>()
                .ForMember(
                    dest => dest.Id,
                    act => act.Ignore()
                )
                .ForMember(
                    dest => dest.OrderId,
                    act => act.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.OrderPrice,
                    act => act.MapFrom(src => src.OrderTotal)
                )
                .ForMember(
                    dest => dest.ShipmentFee,
                    act => act.MapFrom(src => src.ShipCost)
                )
                .ForMember(
                    dest => dest.Total,
                    act => act.MapFrom(src => src.GrandTotal)
                );

            #endregion

            #region Order

            Mapper.CreateMap<Models.Internal.BasketModel, Models.Site.OrderLineViewModel>()
                .ForMember(
                    dest => dest.Tax,
                    act => act.MapFrom(src => src.TotalTax)
                )
                ;

            Mapper.CreateMap<Models.Site.OrderHeaderProcessModel, Order_Heads>();
            Mapper.CreateMap<Models.Site.OrderLineViewModel, Order_Lines>();

            Mapper.CreateMap<Order_Heads, Models.Site.OrderHeaderListViewModel>();

            Mapper.CreateMap<Order_Lines, Models.Site.OrderLineViewModel>()
                .AfterMap((src, dest) => {
                    dest.TotalPrice = src.Quantity * src.Price;
                    dest.ProductImage = src.Catalog_Products.ImageUrl;
                    dest.ProductName = src.Catalog_Products.Title;
                    dest.Tax = (dest.TotalPrice * (decimal)src.TaxRate) / 100;
                });

            Mapper.CreateMap<Order_Heads, Models.Site.OrderHeaderProcessModel>()
                .ForMember(
                    dest => dest.Lines,
                    act => act.MapFrom(src => Mapper.Map<Order_Lines[], List<Models.Site.OrderLineViewModel>>(src.Order_Lines.ToArray()))
                )
                .ForMember(
                    dest => dest.Shipments,
                    act => act.MapFrom(src => Mapper.Map<Shipment_Types[], List<Models.Site.ShipmentTypeViewModel>>(new Shipment_Types[] { src.Shipment_Types }))
                )
                .ForMember(
                    dest => dest.Payments,
                    act => act.MapFrom(src => Mapper.Map<Payment_Types[], List<Models.Site.PaymentTypeViewModel>>(new Payment_Types[] { src.Payment_Entities.FirstOrDefault().Payment_Types }))
                )
                ;

            Mapper.CreateMap<Order_Heads, Models.Site.OrderHeaderDetailViewModel>()
                .ForMember(
                    dest => dest.PaymentFee,
                    act => act.MapFrom(src => src.PaymentFee + src.InstallmentFee)
                )
                .ForMember(
                    dest => dest.StatusText,
                    act => act.MapFrom(src => Models.Common.OrderHeadsListProvider.GetStatuses()[src.Status])
                )
                .ForMember(
                    dest => dest.ShipmentAddress,
                    act => act.MapFrom(src => Mapper.Map<Models.Site.CustomerAddressViewModel>(src.Customer_Addresses))
                )
                .ForMember(
                    dest => dest.InvoiceAddress,
                    act => act.MapFrom(src => Mapper.Map<Models.Site.CustomerAddressViewModel>(src.Customer_Addresses1))
                )
                .ForMember(
                    dest => dest.Shipment,
                    act => act.MapFrom(src => Mapper.Map<Models.Site.ShipmentTypeViewModel>(src.Shipment_Types))
                )
                .ForMember(
                    dest => dest.PaymentTypes,
                    act => act.MapFrom(src => Mapper.Map<Payment_Types[], List<Models.Site.PaymentTypeViewModel>>(src.Payment_Entities.Select(m => m.Payment_Types).ToArray()))
                )
                .ForMember(
                    dest => dest.Lines,
                    act => act.MapFrom(src => Mapper.Map<Order_Lines[], List<Models.Site.OrderLineViewModel>>(src.Order_Lines.ToArray()))
                )
                .ForMember(
                    dest => dest.PaymentEntities,
                    act => act.MapFrom(src => Mapper.Map<Payment_Entities[], List<Models.Site.PaymentEntityViewModel>>(src.Payment_Entities.ToArray()))
                )
                ;


            #endregion

            #region Shipment

            Mapper.CreateMap<Shipment_Types, Models.Site.ShipmentTypeViewModel>()
                .AfterMap(
                    (src, dest) => dest.Price = Helpers.Contents.ShippingHelper.ShippingPrice(dest)
                );

            #endregion

            #region Pages

            Mapper.CreateMap<Pages, Models.Site.PageDetailViewModel>();

            Mapper.CreateMap<Pages, Models.Site.PageMenuViewModel>();

            #endregion

            #region Pos Settings

            Mapper.CreateMap<Pos_Settings, Models.Site.PosSettingViewModel>()
            .AfterMap((src, dest) => 
            {
                dest.Parameters = KPSerializer.Serializer.Deserialize(src.Data);
            });

            #endregion

            #region Slider

            Mapper.CreateMap<Slider, Models.Site.SliderSliderViewModel>();

            Mapper.CreateMap<Slider, Models.Site.SliderDetailViewModel>();

            #endregion

            #region Excel Product

            Mapper.CreateMap<Catalog_Products, Areas.Admin.Models.CatalogProductsExcelModel>()
                .ForMember(dest => dest.AnaSayfadaGoster, act => act.MapFrom(src => src.IsHomepage))
                .ForMember(dest => dest.AramaTagi, act => act.MapFrom(src => src.SearchText))
                .ForMember(dest => dest.Barkod, act => act.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.Dara, act => act.MapFrom(src => src.Tare))
                .ForMember(dest => dest.Detay, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Durum, act => act.MapFrom(src => src.Status))
                .ForMember(dest => dest.Fiyat1, act => act.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Fiyat2, act => act.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Fiyat3, act => act.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Fiyat4, act => act.MapFrom(src => src.Price4))
                .ForMember(dest => dest.Fiyat5, act => act.MapFrom(src => src.Price5))
                .ForMember(dest => dest.Kategori, act => act.MapFrom(src => src.Catalog_Categories.Title))
                .ForMember(dest => dest.KisaAciklama, act => act.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.MaksimumTaksit, act => act.MapFrom(src => src.MaxInstallment))
                .ForMember(dest => dest.Marka, act => act.MapFrom(src => src.Catalog_Brands == null ? "" : src.Catalog_Brands.Title))
                .ForMember(dest => dest.MetaDescription, act => act.MapFrom(src => src.MetaDescription))
                .ForMember(dest => dest.MetaKeywords, act => act.MapFrom(src => src.MetaKeywords))
                .ForMember(dest => dest.MetaTitle, act => act.MapFrom(src => src.MetaTitle))
                .ForMember(dest => dest.NakliyeEdilebilir, act => act.MapFrom(src => src.IsShipable))
                .ForMember(dest => dest.Ozellikli, act => act.MapFrom(src => src.IsFeatured))
                .ForMember(dest => dest.Resim, act => act.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Satilabilir, act => act.MapFrom(src => src.IsBuyable))
                .ForMember(dest => dest.StokKodu, act => act.MapFrom(src => src.SKU))
                .ForMember(dest => dest.StokMiktari, act => act.MapFrom(src => src.Stock))
                .ForMember(dest => dest.Tanim, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.UcretsizNakliye, act => act.MapFrom(src => src.IsFreeShip))
                .ForMember(dest => dest.UreticiUrunKodu, act => act.MapFrom(src => src.MPN))
                .ForMember(dest => dest.UrunKodu, act => act.MapFrom(src => src.ProductCode))
                .ForMember(dest => dest.VergiGrubu, act => act.MapFrom(src => src.Tax_Products.Title))
                .ForMember(dest => dest.Yeni, act => act.MapFrom(src => src.IsNew))
                ;

            Mapper.CreateMap<Areas.Admin.Models.CatalogProductsExcelModel, Catalog_Products>()
                .ForMember(dest => dest.Barcode, act => act.MapFrom(src => src.Barkod))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Detay))
                .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.Resim))
                .ForMember(dest => dest.IsBuyable, act => act.MapFrom(src => src.Satilabilir))
                .ForMember(dest => dest.IsFeatured, act => act.MapFrom(src => src.Ozellikli))
                .ForMember(dest => dest.IsFreeShip, act => act.MapFrom(src => src.UcretsizNakliye))
                .ForMember(dest => dest.IsHomepage, act => act.MapFrom(src => src.AnaSayfadaGoster))
                .ForMember(dest => dest.IsNew, act => act.MapFrom(src => src.Yeni))
                .ForMember(dest => dest.IsShipable, act => act.MapFrom(src => src.NakliyeEdilebilir))
                .ForMember(dest => dest.MaxInstallment, act => act.MapFrom(src => src.MaksimumTaksit))
                .ForMember(dest => dest.MetaDescription, act => act.MapFrom(src => src.MetaDescription))
                .ForMember(dest => dest.MetaKeywords, act => act.MapFrom(src => src.MetaKeywords))
                .ForMember(dest => dest.MetaTitle, act => act.MapFrom(src => src.MetaTitle))
                .ForMember(dest => dest.MPN, act => act.MapFrom(src => src.UreticiUrunKodu))
                .ForMember(dest => dest.Price1, act => act.MapFrom(src => src.Fiyat1))
                .ForMember(dest => dest.Price2, act => act.MapFrom(src => src.Fiyat2))
                .ForMember(dest => dest.Price3, act => act.MapFrom(src => src.Fiyat3))
                .ForMember(dest => dest.Price4, act => act.MapFrom(src => src.Fiyat4))
                .ForMember(dest => dest.Price5, act => act.MapFrom(src => src.Fiyat5))
                .ForMember(dest => dest.ProductCode, act => act.MapFrom(src => src.UrunKodu))
                .ForMember(dest => dest.SearchText, act => act.MapFrom(src => src.AramaTagi))
                .ForMember(dest => dest.ShortDescription, act => act.MapFrom(src => src.KisaAciklama))
                .ForMember(dest => dest.SKU, act => act.MapFrom(src => src.StokKodu))
                .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Durum))
                .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.StokMiktari))
                .ForMember(dest => dest.Tare, act => act.MapFrom(src => src.Dara))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Tanim))
                ;

            #endregion

            #region Catalogs

            Mapper.CreateMap<Catalog_Categories, Models.Site.CatalogCategoryHomeListViewModel>()
                .ForMember(dest => dest.Products, act => act.MapFrom(
                    src => Mapper.Map<Catalog_Products[], List<Models.Site.ProductListDisplayModel>>(
                        src.Catalog_Products.Where(w => w.IsHomepage == true).ToArray()
                        )
                    )
                )
                ;

            #endregion

        }
    }
}