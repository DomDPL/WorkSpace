using DPLRef.eCommerce.Accessors.EntityFramework;
using System;
using System.Linq;
using Seller = DPLRef.eCommerce.Accessors.DataTransferObjects.Seller;

namespace DPLRef.eCommerce.Accessors.Remittance
{
    internal class SellerAccessor : AccessorBase, ISellerAccessor
    {
        public Seller Find(int id)
        {
            using (var db = eCommerceDbContext.Create())
            {
                var model = (from s in db.Sellers
                        where s.Id == id
                        select s)
                    .FirstOrDefault();

                if (model != null)
                {
                    var seller = new Seller();
                    DTOMapper.Map(model, seller);
                    return seller;
                }
            }

            return null;
        }

        public Seller Save(Seller seller)
        {
            EntityFramework.Seller model = null;
            using (var db = eCommerceDbContext.Create())
            {
                if (seller.Id > 0)
                {
                    model = db.Sellers.Find(seller.Id);
                    if (model == null)
                    {
                        // this state should never happen
                        throw new ArgumentException($"Trying to update Seller ({seller.Id}) that does not exist");
                    }

                    DTOMapper.Map(seller, model);
                }
                else
                {
                    model = new EntityFramework.Seller();
                    DTOMapper.Map(seller, model);
                    _ = db.Sellers.Add(model);
                }

                _ = db.SaveChanges();
            }

            return model != null ? Find(model.Id) : null;
        }

        public void Delete(int id)
        {
            using var db = eCommerceDbContext.Create();
            var model = (from s in db.Sellers
                         where s.Id == id
                         select s)
                .FirstOrDefault();

            if (model != null)
            {
                _ = db.Sellers.Remove(model);
                _ = db.SaveChanges();
            }
        }
    }
}