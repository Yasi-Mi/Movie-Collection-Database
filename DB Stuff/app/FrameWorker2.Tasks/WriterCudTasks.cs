using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using MovieDatabase.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace MovieDatabase.Tasks
{
    public class WriterCudTasks : BaseEntityCudTasks<Writer, EditWriterViewModel>
    {
        public WriterCudTasks(IRepository<Writer> itemRepository)
            : base(itemRepository)
        {
        }

        protected override void TransferFormValuesTo(Writer toUpdate, Writer fromForm)
        {
            toUpdate.FirstName = fromForm.FirstName;
            toUpdate.LastName = fromForm.LastName;
        }
    }
}
