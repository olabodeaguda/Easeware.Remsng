using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Repositories;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Managers
{
    public class TaxpayerManager : ITaxpayerManager
    {
        private ITaxpayerRepository _tpRepo;
        public TaxpayerManager(ITaxpayerRepository taxpayerRepository)
        {
            _tpRepo = taxpayerRepository;
        }
        public Task<TaxpayerModel> CreateTaxpayer(TaxpayerModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PageModel> Get(long lcdaId, PageModel pageModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaxpayerModel>> GetAsync(long streetId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaxpayerModel>> GetByLcda(long lcdaId)
        {
            throw new NotImplementedException();
        }

        public Task<TaxpayerModel> UpdateStatus(TaxpayerModel tm)
        {
            throw new NotImplementedException();
        }

        public Task<TaxpayerModel> UpdateTaxpayer(TaxpayerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
