﻿#region License
// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.EncounterModule
{
    /// <summary>
    /// It implements the factory utilities of the encounter object.
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceRepository _serviceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceFactory"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository.</param>
        public ServiceFactory (IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Creates the service.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        /// <param name="diagnosis">The diagnosis.</param>
        /// <param name="medicalProcedure">The medical procedure.</param>
        /// <param name="primaryIndicator">If set to <c>true</c> [primary indicator].</param>
        /// <param name="trackingNumber">The tracking number.</param>
        /// <returns>A service.</returns>
        public Service CreateService ( Encounter encounter, CodedConcept diagnosis, MedicalProcedure medicalProcedure, bool primaryIndicator, long trackingNumber )
        {
            var service = new Service(encounter, diagnosis, medicalProcedure, primaryIndicator, trackingNumber);
            encounter.AddService ( service );
            _serviceRepository.MakePersistent(service);
            return service;
        }
    }
}
