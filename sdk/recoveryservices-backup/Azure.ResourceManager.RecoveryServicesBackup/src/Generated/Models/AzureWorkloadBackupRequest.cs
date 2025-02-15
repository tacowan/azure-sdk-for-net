// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.RecoveryServicesBackup.Models
{
    /// <summary> AzureWorkload workload-specific backup request. </summary>
    public partial class AzureWorkloadBackupRequest : BackupRequest
    {
        /// <summary> Initializes a new instance of AzureWorkloadBackupRequest. </summary>
        public AzureWorkloadBackupRequest()
        {
            ObjectType = "AzureWorkloadBackupRequest";
        }

        /// <summary> Initializes a new instance of AzureWorkloadBackupRequest. </summary>
        /// <param name="objectType"> This property will be used as the discriminator for deciding the specific types in the polymorphic chain of types. </param>
        /// <param name="backupType"> Type of backup, viz. Full, Differential, Log or CopyOnlyFull. </param>
        /// <param name="enableCompression"> Bool for Compression setting. </param>
        /// <param name="recoveryPointExpiryTimeInUTC"> Backup copy will expire after the time specified (UTC). </param>
        internal AzureWorkloadBackupRequest(string objectType, BackupType? backupType, bool? enableCompression, DateTimeOffset? recoveryPointExpiryTimeInUTC) : base(objectType)
        {
            BackupType = backupType;
            EnableCompression = enableCompression;
            RecoveryPointExpiryTimeInUTC = recoveryPointExpiryTimeInUTC;
            ObjectType = objectType ?? "AzureWorkloadBackupRequest";
        }

        /// <summary> Type of backup, viz. Full, Differential, Log or CopyOnlyFull. </summary>
        public BackupType? BackupType { get; set; }
        /// <summary> Bool for Compression setting. </summary>
        public bool? EnableCompression { get; set; }
        /// <summary> Backup copy will expire after the time specified (UTC). </summary>
        public DateTimeOffset? RecoveryPointExpiryTimeInUTC { get; set; }
    }
}
