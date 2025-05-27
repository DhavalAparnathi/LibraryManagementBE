using Library.Business.ViewModel;
using Library.Models.DaysOfWeek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Provider
{
    public interface ITimetableProvider
    {
        List<DaysOfWeek> GetDaysOfWeek();
    }
}
