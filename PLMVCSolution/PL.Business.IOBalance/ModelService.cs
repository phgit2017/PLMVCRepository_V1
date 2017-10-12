using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;
using PL.Business.IOBalance.Extensions;
//-- Core
using PL.Core.Entity.IOBalanceDB;
using PL.Core.Entity.Repository.Interface;
using IOBalanceEntity = PL.Core.Entity.IOBalanceDB;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;
namespace PL.Business.IOBalance
{
    public class ModelService : IModelService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Model> _model;

        IOBalanceEntity.Model model;
        public ModelService(IIOBalanceRepository<Model> model)
        {
            this._model = model;
            this.model = new IOBalanceEntity.Model();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations
        public IQueryable<ModelDto> GetAll()
        {
            var list = from m in _model.GetAll()
                       select new ModelDto() 
                       {
                           ModelID = m.ModelID,
                           ModelName = m.ModelName
                       };

            return list;
        }

        public ModelDto FindModelById(int modelId)
        {
            var list = GetAll().Where(m => m.ModelID == modelId).FirstOrDefault();
            return list;
        }

        public bool SaveModel(ModelDto modelDetails)
        {
            this.model = modelDetails.DtoToEntity();

            if (this._model.Insert(this.model).IsNull())
            {
                return false;
            }
            return true;
        }

        public bool UpdateModel(ModelDto newModelDetails)
        {
            var updatedModel = this.model;

            updatedModel = new Model()
            {
                ModelID = newModelDetails.ModelID,
                ModelName = newModelDetails.ModelName
            };

            if (this._model.Update2(updatedModel).IsNull())
            {
                return false;
            }

            return true;
        }
        #endregion InterfaceImplementations
        
    }
}
