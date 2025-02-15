// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.RecoveryServicesSiteRecovery.Models
{
    /// <summary> Enable protection input properties. </summary>
    public partial class EnableProtectionInputProperties
    {
        /// <summary> Initializes a new instance of EnableProtectionInputProperties. </summary>
        public EnableProtectionInputProperties()
        {
        }

        /// <summary> The Policy Id. </summary>
        public string PolicyId { get; set; }
        /// <summary> The protectable item Id. </summary>
        public string ProtectableItemId { get; set; }
        /// <summary>
        /// The ReplicationProviderInput. For HyperVReplicaAzure provider, it will be AzureEnableProtectionInput object. For San provider, it will be SanEnableProtectionInput object. For HyperVReplicaAzure provider, it can be null.
        /// Please note <see cref="EnableProtectionProviderSpecificInput"/> is the base class. According to the scenario, a derived class of the base class might need to be assigned here, or this property needs to be casted to one of the possible derived classes.
        /// The available derived classes include <see cref="A2AEnableProtectionInput"/>, <see cref="A2ACrossClusterMigrationEnableProtectionInput"/>, <see cref="HyperVReplicaAzureEnableProtectionInput"/>, <see cref="InMageEnableProtectionInput"/>, <see cref="InMageAzureV2EnableProtectionInput"/> and <see cref="InMageRcmEnableProtectionInput"/>.
        /// </summary>
        public EnableProtectionProviderSpecificInput ProviderSpecificDetails { get; set; }
    }
}
