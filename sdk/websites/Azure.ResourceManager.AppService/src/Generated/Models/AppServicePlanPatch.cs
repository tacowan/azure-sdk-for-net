// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using Azure.Core;
using Azure.ResourceManager.Models;

namespace Azure.ResourceManager.AppService.Models
{
    /// <summary> ARM resource for a app service plan. </summary>
    public partial class AppServicePlanPatch : ResourceData
    {
        /// <summary> Initializes a new instance of AppServicePlanPatch. </summary>
        public AppServicePlanPatch()
        {
        }

        /// <summary> Initializes a new instance of AppServicePlanPatch. </summary>
        /// <param name="id"> The id. </param>
        /// <param name="name"> The name. </param>
        /// <param name="resourceType"> The resourceType. </param>
        /// <param name="systemData"> The systemData. </param>
        /// <param name="workerTierName"> Target worker tier assigned to the App Service plan. </param>
        /// <param name="status"> App Service plan status. </param>
        /// <param name="subscription"> App Service plan subscription. </param>
        /// <param name="hostingEnvironmentProfile"> Specification for the App Service Environment to use for the App Service plan. </param>
        /// <param name="maximumNumberOfWorkers"> Maximum number of instances that can be assigned to this App Service plan. </param>
        /// <param name="geoRegion"> Geographical location for the App Service plan. </param>
        /// <param name="isPerSiteScaling">
        /// If &lt;code&gt;true&lt;/code&gt;, apps assigned to this App Service plan can be scaled independently.
        /// If &lt;code&gt;false&lt;/code&gt;, apps assigned to this App Service plan will scale to all instances of the plan.
        /// </param>
        /// <param name="isElasticScaleEnabled"> ServerFarm supports ElasticScale. Apps in this plan will scale as if the ServerFarm was ElasticPremium sku. </param>
        /// <param name="maximumElasticWorkerCount"> Maximum number of total workers allowed for this ElasticScaleEnabled App Service Plan. </param>
        /// <param name="numberOfSites"> Number of apps assigned to this App Service plan. </param>
        /// <param name="isSpot"> If &lt;code&gt;true&lt;/code&gt;, this App Service Plan owns spot instances. </param>
        /// <param name="spotExpirationOn"> The time when the server farm expires. Valid only if it is a spot server farm. </param>
        /// <param name="freeOfferExpirationOn"> The time when the server farm free offer expires. </param>
        /// <param name="resourceGroup"> Resource group of the App Service plan. </param>
        /// <param name="isReserved"> If Linux app service plan &lt;code&gt;true&lt;/code&gt;, &lt;code&gt;false&lt;/code&gt; otherwise. </param>
        /// <param name="isXenon"> Obsolete: If Hyper-V container app service plan &lt;code&gt;true&lt;/code&gt;, &lt;code&gt;false&lt;/code&gt; otherwise. </param>
        /// <param name="isHyperV"> If Hyper-V container app service plan &lt;code&gt;true&lt;/code&gt;, &lt;code&gt;false&lt;/code&gt; otherwise. </param>
        /// <param name="targetWorkerCount"> Scaling worker count. </param>
        /// <param name="targetWorkerSizeId"> Scaling worker size ID. </param>
        /// <param name="provisioningState"> Provisioning state of the App Service Plan. </param>
        /// <param name="kubeEnvironmentProfile"> Specification for the Kubernetes Environment to use for the App Service plan. </param>
        /// <param name="isZoneRedundant">
        /// If &lt;code&gt;true&lt;/code&gt;, this App Service Plan will perform availability zone balancing.
        /// If &lt;code&gt;false&lt;/code&gt;, this App Service Plan will not perform availability zone balancing.
        /// </param>
        /// <param name="kind"> Kind of resource. </param>
        internal AppServicePlanPatch(ResourceIdentifier id, string name, ResourceType resourceType, SystemData systemData, string workerTierName, AppServicePlanStatus? status, string subscription, HostingEnvironmentProfile hostingEnvironmentProfile, int? maximumNumberOfWorkers, string geoRegion, bool? isPerSiteScaling, bool? isElasticScaleEnabled, int? maximumElasticWorkerCount, int? numberOfSites, bool? isSpot, DateTimeOffset? spotExpirationOn, DateTimeOffset? freeOfferExpirationOn, string resourceGroup, bool? isReserved, bool? isXenon, bool? isHyperV, int? targetWorkerCount, int? targetWorkerSizeId, ProvisioningState? provisioningState, KubeEnvironmentProfile kubeEnvironmentProfile, bool? isZoneRedundant, string kind) : base(id, name, resourceType, systemData)
        {
            WorkerTierName = workerTierName;
            Status = status;
            Subscription = subscription;
            HostingEnvironmentProfile = hostingEnvironmentProfile;
            MaximumNumberOfWorkers = maximumNumberOfWorkers;
            GeoRegion = geoRegion;
            IsPerSiteScaling = isPerSiteScaling;
            IsElasticScaleEnabled = isElasticScaleEnabled;
            MaximumElasticWorkerCount = maximumElasticWorkerCount;
            NumberOfSites = numberOfSites;
            IsSpot = isSpot;
            SpotExpirationOn = spotExpirationOn;
            FreeOfferExpirationOn = freeOfferExpirationOn;
            ResourceGroup = resourceGroup;
            IsReserved = isReserved;
            IsXenon = isXenon;
            IsHyperV = isHyperV;
            TargetWorkerCount = targetWorkerCount;
            TargetWorkerSizeId = targetWorkerSizeId;
            ProvisioningState = provisioningState;
            KubeEnvironmentProfile = kubeEnvironmentProfile;
            IsZoneRedundant = isZoneRedundant;
            Kind = kind;
        }

        /// <summary> Target worker tier assigned to the App Service plan. </summary>
        public string WorkerTierName { get; set; }
        /// <summary> App Service plan status. </summary>
        public AppServicePlanStatus? Status { get; }
        /// <summary> App Service plan subscription. </summary>
        public string Subscription { get; }
        /// <summary> Specification for the App Service Environment to use for the App Service plan. </summary>
        public HostingEnvironmentProfile HostingEnvironmentProfile { get; set; }
        /// <summary> Maximum number of instances that can be assigned to this App Service plan. </summary>
        public int? MaximumNumberOfWorkers { get; }
        /// <summary> Geographical location for the App Service plan. </summary>
        public string GeoRegion { get; }
        /// <summary>
        /// If &lt;code&gt;true&lt;/code&gt;, apps assigned to this App Service plan can be scaled independently.
        /// If &lt;code&gt;false&lt;/code&gt;, apps assigned to this App Service plan will scale to all instances of the plan.
        /// </summary>
        public bool? IsPerSiteScaling { get; set; }
        /// <summary> ServerFarm supports ElasticScale. Apps in this plan will scale as if the ServerFarm was ElasticPremium sku. </summary>
        public bool? IsElasticScaleEnabled { get; set; }
        /// <summary> Maximum number of total workers allowed for this ElasticScaleEnabled App Service Plan. </summary>
        public int? MaximumElasticWorkerCount { get; set; }
        /// <summary> Number of apps assigned to this App Service plan. </summary>
        public int? NumberOfSites { get; }
        /// <summary> If &lt;code&gt;true&lt;/code&gt;, this App Service Plan owns spot instances. </summary>
        public bool? IsSpot { get; set; }
        /// <summary> The time when the server farm expires. Valid only if it is a spot server farm. </summary>
        public DateTimeOffset? SpotExpirationOn { get; set; }
        /// <summary> The time when the server farm free offer expires. </summary>
        public DateTimeOffset? FreeOfferExpirationOn { get; set; }
        /// <summary> Resource group of the App Service plan. </summary>
        public string ResourceGroup { get; }
        /// <summary> If Linux app service plan &lt;code&gt;true&lt;/code&gt;, &lt;code&gt;false&lt;/code&gt; otherwise. </summary>
        public bool? IsReserved { get; set; }
        /// <summary> Obsolete: If Hyper-V container app service plan &lt;code&gt;true&lt;/code&gt;, &lt;code&gt;false&lt;/code&gt; otherwise. </summary>
        public bool? IsXenon { get; set; }
        /// <summary> If Hyper-V container app service plan &lt;code&gt;true&lt;/code&gt;, &lt;code&gt;false&lt;/code&gt; otherwise. </summary>
        public bool? IsHyperV { get; set; }
        /// <summary> Scaling worker count. </summary>
        public int? TargetWorkerCount { get; set; }
        /// <summary> Scaling worker size ID. </summary>
        public int? TargetWorkerSizeId { get; set; }
        /// <summary> Provisioning state of the App Service Plan. </summary>
        public ProvisioningState? ProvisioningState { get; }
        /// <summary> Specification for the Kubernetes Environment to use for the App Service plan. </summary>
        public KubeEnvironmentProfile KubeEnvironmentProfile { get; set; }
        /// <summary>
        /// If &lt;code&gt;true&lt;/code&gt;, this App Service Plan will perform availability zone balancing.
        /// If &lt;code&gt;false&lt;/code&gt;, this App Service Plan will not perform availability zone balancing.
        /// </summary>
        public bool? IsZoneRedundant { get; set; }
        /// <summary> Kind of resource. </summary>
        public string Kind { get; set; }
    }
}
