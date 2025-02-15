// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Core;

namespace Azure.AI.AnomalyDetector
{
    /// <summary> The response of entire anomaly detection. </summary>
    public partial class UnivariateEntireDetectionResult
    {
        /// <summary> Initializes a new instance of UnivariateEntireDetectionResult. </summary>
        /// <param name="period"></param>
        /// <param name="expectedValues"></param>
        /// <param name="upperMargins"></param>
        /// <param name="lowerMargins"></param>
        /// <param name="isAnomaly"></param>
        /// <param name="isNegativeAnomaly"></param>
        /// <param name="isPositiveAnomaly"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="expectedValues"/>, <paramref name="upperMargins"/>, <paramref name="lowerMargins"/>, <paramref name="isAnomaly"/>, <paramref name="isNegativeAnomaly"/> or <paramref name="isPositiveAnomaly"/> is null. </exception>
        internal UnivariateEntireDetectionResult(int period, IEnumerable<float> expectedValues, IEnumerable<float> upperMargins, IEnumerable<float> lowerMargins, IEnumerable<bool> isAnomaly, IEnumerable<bool> isNegativeAnomaly, IEnumerable<bool> isPositiveAnomaly)
        {
            Argument.AssertNotNull(expectedValues, nameof(expectedValues));
            Argument.AssertNotNull(upperMargins, nameof(upperMargins));
            Argument.AssertNotNull(lowerMargins, nameof(lowerMargins));
            Argument.AssertNotNull(isAnomaly, nameof(isAnomaly));
            Argument.AssertNotNull(isNegativeAnomaly, nameof(isNegativeAnomaly));
            Argument.AssertNotNull(isPositiveAnomaly, nameof(isPositiveAnomaly));

            Period = period;
            ExpectedValues = expectedValues.ToList();
            UpperMargins = upperMargins.ToList();
            LowerMargins = lowerMargins.ToList();
            IsAnomaly = isAnomaly.ToList();
            IsNegativeAnomaly = isNegativeAnomaly.ToList();
            IsPositiveAnomaly = isPositiveAnomaly.ToList();
            Severity = new ChangeTrackingList<float>();
        }

        /// <summary> Initializes a new instance of UnivariateEntireDetectionResult. </summary>
        /// <param name="period"></param>
        /// <param name="expectedValues"></param>
        /// <param name="upperMargins"></param>
        /// <param name="lowerMargins"></param>
        /// <param name="isAnomaly"></param>
        /// <param name="isNegativeAnomaly"></param>
        /// <param name="isPositiveAnomaly"></param>
        /// <param name="severity"></param>
        internal UnivariateEntireDetectionResult(int period, IReadOnlyList<float> expectedValues, IReadOnlyList<float> upperMargins, IReadOnlyList<float> lowerMargins, IReadOnlyList<bool> isAnomaly, IReadOnlyList<bool> isNegativeAnomaly, IReadOnlyList<bool> isPositiveAnomaly, IReadOnlyList<float> severity)
        {
            Period = period;
            ExpectedValues = expectedValues.ToList();
            UpperMargins = upperMargins.ToList();
            LowerMargins = lowerMargins.ToList();
            IsAnomaly = isAnomaly.ToList();
            IsNegativeAnomaly = isNegativeAnomaly.ToList();
            IsPositiveAnomaly = isPositiveAnomaly.ToList();
            Severity = severity.ToList();
        }

        /// <summary> Gets the period. </summary>
        public int Period { get; }
        /// <summary> Gets the expected values. </summary>
        public IReadOnlyList<float> ExpectedValues { get; }
        /// <summary> Gets the upper margins. </summary>
        public IReadOnlyList<float> UpperMargins { get; }
        /// <summary> Gets the lower margins. </summary>
        public IReadOnlyList<float> LowerMargins { get; }
        /// <summary> Gets the is anomaly. </summary>
        public IReadOnlyList<bool> IsAnomaly { get; }
        /// <summary> Gets the is negative anomaly. </summary>
        public IReadOnlyList<bool> IsNegativeAnomaly { get; }
        /// <summary> Gets the is positive anomaly. </summary>
        public IReadOnlyList<bool> IsPositiveAnomaly { get; }
        /// <summary> Gets the severity. </summary>
        public IReadOnlyList<float> Severity { get; }
    }
}
