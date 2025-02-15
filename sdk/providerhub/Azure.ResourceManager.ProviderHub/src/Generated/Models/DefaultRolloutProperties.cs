// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.ProviderHub.Models
{
    /// <summary> The DefaultRolloutProperties. </summary>
    public partial class DefaultRolloutProperties
    {
        /// <summary> Initializes a new instance of DefaultRolloutProperties. </summary>
        public DefaultRolloutProperties()
        {
        }

        /// <summary> Initializes a new instance of DefaultRolloutProperties. </summary>
        /// <param name="provisioningState"> The provisioned state of the resource. </param>
        /// <param name="specification"></param>
        /// <param name="status"></param>
        internal DefaultRolloutProperties(ProvisioningState? provisioningState, DefaultRolloutPropertiesSpecification specification, DefaultRolloutPropertiesStatus status)
        {
            ProvisioningState = provisioningState;
            Specification = specification;
            Status = status;
        }

        /// <summary> The provisioned state of the resource. </summary>
        public ProvisioningState? ProvisioningState { get; set; }
        /// <summary> Gets or sets the specification. </summary>
        public DefaultRolloutPropertiesSpecification Specification { get; set; }
        /// <summary> Gets or sets the status. </summary>
        public DefaultRolloutPropertiesStatus Status { get; set; }
    }
}
