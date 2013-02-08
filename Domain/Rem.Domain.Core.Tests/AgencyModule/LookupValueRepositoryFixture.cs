using System;
using System.Collections.Generic;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    public class LookupValueRepositoryFixture : ILookupValueRepository
    {
        public IList<LookupBase> GetAll ( Type type )
        {
            var lookupList = new List<LookupBase>();
            if (type == typeof(StaffEventType))
            {
                var staffEventTypes = GetAllStaffEventTypes();
                lookupList.AddRange ( staffEventTypes );
            }
            else if (type == typeof(StaffChecklistItemType))
            {
                var staffChecklistItemTypes = GetAllStaffChecklistItemTypes();
                lookupList.AddRange(staffChecklistItemTypes);
            }

            return lookupList;
        }

        public IList<StaffEventType> GetAllStaffEventTypes()
        {
            return new List<StaffEventType>
                       {
                           new StaffEventType {Name= "Development Plan"},
                           new StaffEventType {Name= "Performance Review"}
                       };
        }

        public IList<StaffChecklistItemType> GetAllStaffChecklistItemTypes()
        {
            return new List<StaffChecklistItemType>
                       {
                           new StaffChecklistItemType { Name = "Performance Appraisal Participation" },
                           new StaffChecklistItemType { Name = "Policies and Procedure Manual has been reviewed" }
                       };
        }

        public LookupBase GetLookupByWellKnownName ( Type type, string wellKnownName )
        {
            throw new NotImplementedException ();
        }

        public LookupBase GetLookupByName(Type type, string name)
        {
            throw new NotImplementedException();
        }

        public TLookup GetLookupByWellKnownName<TLookup> ( string wellKnownName ) where TLookup : LookupBase
        {
            throw new NotImplementedException ();
        }

        public TLookup GetLookupByName<TLookup>(string name) where TLookup : LookupBase
        {
            throw new NotImplementedException();
        }

        public LookupBase GetLookupByKey ( Type type, long key )
        {
            throw new NotImplementedException ();
        }

        public TLookup GetLookupByKey<TLookup> ( long key ) where TLookup : LookupBase
        {
            throw new NotImplementedException ();
        }

        public Tuple<int, int, List<LookupBase>> FindLookupValueListByKeywords(Type lookupType, string searchCriteria, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}