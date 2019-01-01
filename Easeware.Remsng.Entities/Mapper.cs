using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Easeware.Remsng.Entities.Entities;

namespace Easeware.Remsng.Entities
{
    public static class Mapper
    {
        #region User Model
        public static User Map(this UserModel userModel)
        {
            if (userModel == null)
            {
                return null;
            }

            return new User()
            {
                CreatedBy = userModel.CreatedBy,
                CreatedDate = userModel.CreatedDate,
                email = userModel.email,
                gender = userModel.gender.ToString(),
                id = userModel.id,
                lastname = userModel.lastname,
                lockedoutCount = userModel.lockedoutCount,
                lockedoutenabled = userModel.lockedoutenabled,
                lockedOutEndDateUTC = userModel.lockedOutEndDateUTC,
                ModifiedBy = userModel.ModifiedBy,
                ModifiedDate = userModel.ModifiedDate,
                otherNames = userModel.otherNames,
                passwordHash = userModel.passwordHash,
                securityStamp = userModel.securityStamp,
                userStatus = userModel.userStatus.ToString()
            };
        }

        public static UserModel Map(this User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserModel()
            {
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                email = user.email,
                gender = user.gender.ToEnum<Gender>(),
                id = user.id,
                lastname = user.lastname,
                lockedoutCount = user.lockedoutCount,
                lockedoutenabled = user.lockedoutenabled,
                lockedOutEndDateUTC = user.lockedOutEndDateUTC,
                ModifiedBy = user.ModifiedBy,
                ModifiedDate = user.ModifiedDate,
                otherNames = user.otherNames,
                passwordHash = user.passwordHash,
                securityStamp = user.securityStamp,
                userStatus = user.userStatus.ToEnum<UserStatus>()
            };
        }
        #endregion

        #region LCDAModel
        public static LcdaModel Map(this Lcda lcda)
        {
            if (lcda == null)
            {
                return null;
            }

            return new LcdaModel()
            {
                CreatedBy = lcda.CreatedBy,
                CreatedDate = lcda.CreatedDate,
                Id = lcda.Id,
                LcdaCode = lcda.LcdaCode,
                LcdaName = lcda.LcdaName,
                LcdaStatus = lcda.LcdaStatus.ToEnum<LcdaStatus>(),
                ModifiedBy = lcda.ModifiedBy,
                ModifiedDate = lcda.ModifiedDate
            };
        }

        public static Lcda Map(this LcdaModel lcda)
        {
            if (lcda == null)
            {
                return null;
            }

            return new Lcda()
            {
                CreatedBy = lcda.CreatedBy,
                CreatedDate = lcda.CreatedDate,
                Id = lcda.Id,
                LcdaCode = lcda.LcdaCode,
                LcdaName = lcda.LcdaName,
                LcdaStatus = lcda.LcdaStatus.ToString(),
                ModifiedBy = lcda.ModifiedBy,
                ModifiedDate = lcda.ModifiedDate
            };
        }

        #endregion

        #region Ward Map
        public static WardModel Map(this Ward ward)
        {
            if (ward == null)
            {
                return null;
            }

            return new WardModel()
            {
                CreatedBy = ward.CreatedBy,
                CreatedDate = ward.CreatedDate,
                Id = ward.Id,
                LcdaId = ward.LcdaId,
                ModifiedBy = ward.ModifiedBy,
                ModifiedDate = ward.ModifiedDate,
                Status = ward.Status.ToEnum<WardStatus>(),
                WardCode = ward.WardCode,
                WardName = ward.WardName
            };
        }
        public static Ward Map(this WardModel ward)
        {
            if (ward == null)
            {
                return null;
            }

            return new Ward()
            {
                CreatedBy = ward.CreatedBy,
                CreatedDate = ward.CreatedDate,
                Id = ward.Id,
                LcdaId = ward.LcdaId,
                ModifiedBy = ward.ModifiedBy,
                ModifiedDate = ward.ModifiedDate,
                Status = ward.Status.ToString(),
                WardCode = ward.WardCode,
                WardName = ward.WardName
            };
        }
        #endregion

        #region Street
        public static StreetModel Map(this Street street)
        {
            if (street == null)
            {
                return null;
            }

            return new StreetModel()
            {
                Id = street.Id,
                StreetCode = street.StreetCode,
                StreetName = street.StreetName,
                WardId = street.WardId,
                StreetStatus = street.StreetStatus.ToEnum<StreetStatus>()
            };
        }

        public static Street Map(this StreetModel street)
        {
            if (street == null)
            {
                return null;
            }

            return new Street()
            {
                Id = street.Id,
                StreetCode = street.StreetCode,
                StreetName = street.StreetName,
                WardId = street.WardId,
                StreetStatus = street.StreetStatus.ToString()
            };
        }

        #endregion

        #region Company
        public static Company Map(this CompanyModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Company()
            {
                CompanyCode = model.CompanyCode,
                CompanyName = model.CompanyName,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                LcdaId = model.LcdaId,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                Status = model.Status.ToString()
            };
        }
        public static CompanyModel Map(this Company entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new CompanyModel()
            {
                CompanyCode = entity.CompanyCode,
                CompanyName = entity.CompanyName,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                LcdaId = entity.LcdaId,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Status = entity.Status.ToEnum<CompanyStatus>()
            };
        }
        #endregion

        #region Taxpayer
        public static TaxpayerModel Map(this Taxpayer entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new TaxpayerModel()
            {
                AddressId = entity.AddressId,
                CompanyId = entity.CompanyId,
                Id = entity.Id,
                LastName = entity.LastName,
                OtherNames = entity.OtherNames,
                Status = entity.Status.ToEnum<TaxStatus>(),
                TaxCategory = entity.TaxCategory.ToEnum<TaxCategory>(),

            };
        }
        public static Taxpayer Map(this TaxpayerModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Taxpayer()
            {
                AddressId = model.AddressId,
                CompanyId = model.CompanyId,
                Id = model.Id,
                LastName = model.LastName,
                OtherNames = model.OtherNames,
                Status = model.Status.ToString(),
                TaxCategory = model.TaxCategory.ToString()
            };
        }
        #endregion

        #region Address
        public static Address Map(this AddressModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Address()
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                HouseNumber = model.HouseNumber,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                Status = model.Status.ToString(),
                StreetId = model.StreetId
            };
        }

        public static AddressModel Map(this Address entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new AddressModel()
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                HouseNumber = entity.HouseNumber,
                Id = entity.Id,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Status = entity.Status.ToEnum<AddressStatus>(),
                StreetId = entity.StreetId
            };
        }
        #endregion
    }
}
