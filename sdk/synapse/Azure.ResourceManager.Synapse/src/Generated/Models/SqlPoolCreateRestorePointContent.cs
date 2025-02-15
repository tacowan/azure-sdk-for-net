// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using Azure.Core;

namespace Azure.ResourceManager.Synapse.Models
{
    /// <summary> Contains the information necessary to perform a create Sql pool restore point operation. </summary>
    public partial class SqlPoolCreateRestorePointContent
    {
        /// <summary> Initializes a new instance of SqlPoolCreateRestorePointContent. </summary>
        /// <param name="restorePointLabel"> The restore point label to apply. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="restorePointLabel"/> is null. </exception>
        public SqlPoolCreateRestorePointContent(string restorePointLabel)
        {
            Argument.AssertNotNull(restorePointLabel, nameof(restorePointLabel));

            RestorePointLabel = restorePointLabel;
        }

        /// <summary> The restore point label to apply. </summary>
        public string RestorePointLabel { get; }
    }
}
