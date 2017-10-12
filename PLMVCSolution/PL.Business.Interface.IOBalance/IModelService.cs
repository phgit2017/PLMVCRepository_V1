using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IModelService
    {
        IQueryable<ModelDto> GetAll();
        ModelDto FindModelById(int modelId);
        bool SaveModel(ModelDto modelDetails);
        bool UpdateModel(ModelDto newModelDetails);

    }
}
