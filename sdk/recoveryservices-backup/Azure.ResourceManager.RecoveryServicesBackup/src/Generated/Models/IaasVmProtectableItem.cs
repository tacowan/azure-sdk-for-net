// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.RecoveryServicesBackup.Models
{
    /// <summary>
    /// IaaS VM workload-specific backup item.
    /// Please note <see cref="IaasVmProtectableItem"/> is the base class. According to the scenario, a derived class of the base class might need to be assigned here, or this property needs to be casted to one of the possible derived classes.
    /// The available derived classes include <see cref="AzureIaaSClassicComputeVmProtectableItem"/> and <see cref="AzureIaaSComputeVmProtectableItem"/>.
    /// </summary>
    public partial class IaasVmProtectableItem : WorkloadProtectableItem
    {
        /// <summary> Initializes a new instance of IaasVmProtectableItem. </summary>
        public IaasVmProtectableItem()
        {
            ProtectableItemType = "IaaSVMProtectableItem";
        }

        /// <summary> Initializes a new instance of IaasVmProtectableItem. </summary>
        /// <param name="backupManagementType"> Type of backup management to backup an item. </param>
        /// <param name="workloadType"> Type of workload for the backup management. </param>
        /// <param name="protectableItemType"> Type of the backup item. </param>
        /// <param name="friendlyName"> Friendly name of the backup item. </param>
        /// <param name="protectionState"> State of the back up item. </param>
        /// <param name="virtualMachineId"> Fully qualified ARM ID of the virtual machine. </param>
        /// <param name="virtualMachineVersion"> Specifies whether the container represents a Classic or an Azure Resource Manager VM. </param>
        /// <param name="resourceGroup"> Resource group name of Recovery Services Vault. </param>
        internal IaasVmProtectableItem(string backupManagementType, string workloadType, string protectableItemType, string friendlyName, ProtectionStatus? protectionState, string virtualMachineId, string virtualMachineVersion, string resourceGroup) : base(backupManagementType, workloadType, protectableItemType, friendlyName, protectionState)
        {
            VirtualMachineId = virtualMachineId;
            VirtualMachineVersion = virtualMachineVersion;
            ResourceGroup = resourceGroup;
            ProtectableItemType = protectableItemType ?? "IaaSVMProtectableItem";
        }

        /// <summary> Fully qualified ARM ID of the virtual machine. </summary>
        public string VirtualMachineId { get; set; }
        /// <summary> Specifies whether the container represents a Classic or an Azure Resource Manager VM. </summary>
        public string VirtualMachineVersion { get; set; }
        /// <summary> Resource group name of Recovery Services Vault. </summary>
        public string ResourceGroup { get; set; }
    }
}
