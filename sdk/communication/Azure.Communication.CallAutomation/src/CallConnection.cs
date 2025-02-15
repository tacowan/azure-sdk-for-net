﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Communication.CallAutomation
{
    /// <summary>
    /// The Azure Communication Services Call Connection Client.
    /// </summary>
    public class CallConnection
    {
        private readonly ClientDiagnostics _clientDiagnostics;
        internal CallConnectionRestClient RestClient { get; }
        internal CallMediaRestClient CallMediaRestClient { get; }
        internal EventProcessor EventProcessor { get; }

        /// <summary>
        /// The call connection id.
        /// </summary>
        public virtual string CallConnectionId { get; internal set; }

        internal CallConnection(string callConnectionId, CallConnectionRestClient callConnectionRestClient, CallMediaRestClient callCallMediaRestClient, ClientDiagnostics clientDiagnostics, EventProcessor eventProcessor)
        {
            CallConnectionId = callConnectionId;
            RestClient = callConnectionRestClient;
            CallMediaRestClient = callCallMediaRestClient;
            _clientDiagnostics = clientDiagnostics;
            EventProcessor = eventProcessor;
        }

        /// <summary>Initializes a new instance of <see cref="CallConnection"/> for mocking.</summary>
        protected CallConnection()
        {
            _clientDiagnostics = null;
            RestClient = null;
            CallMediaRestClient = null;
            CallConnectionId = null;
        }

        /// <summary> Get various properties of the call. <see cref="CallConnectionProperties"/>.</summary>
        /// <param name="cancellationToken"> The cancellation token. </param>
        public virtual async Task<Response<CallConnectionProperties>> GetCallConnectionPropertiesAsync(CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallAutomationClient)}.{nameof(GetCallConnectionProperties)}");
            scope.Start();
            try
            {
                var response = await RestClient.GetCallAsync(CallConnectionId, cancellationToken: cancellationToken).ConfigureAwait(false);

                return Response.FromValue(
                    new CallConnectionProperties(response.Value),
                    response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Get various properties of a ongoing call. <see cref="CallConnectionProperties"/>.</summary>
        /// <param name="cancellationToken"> The cancellation token. </param>
        public virtual Response<CallConnectionProperties> GetCallConnectionProperties(CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallAutomationClient)}.{nameof(GetCallConnectionProperties)}");
            scope.Start();
            try
            {
                var response = RestClient.GetCall(CallConnectionId, cancellationToken: cancellationToken);

                return Response.FromValue(
                    new CallConnectionProperties(response.Value),
                    response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Disconnect the current caller in a group-call or end a p2p-call.</summary>
        /// <param name="forEveryone"> If true, this will terminate the call and hang up on all participants in this call. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        public virtual async Task<Response> HangUpAsync(bool forEveryone, CancellationToken cancellationToken = default)
        {
            HangUpOptions options = new HangUpOptions(forEveryone);

            return await HangUpAsync(options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Disconnect the current caller in a group-call or end a p2p-call.</summary>
        /// <param name="options"> Options for the HangUp call operation. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual async Task<Response> HangUpAsync(HangUpOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(HangUp)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                if (options.ForEveryone)
                {
                    options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                    return await RestClient.TerminateCallAsync(
                        CallConnectionId,
                        options.RepeatabilityHeaders?.RepeatabilityRequestId,
                        options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                        cancellationToken
                        ).ConfigureAwait(false);
                }
                else
                {
                    return await RestClient.HangupCallAsync(
                        callConnectionId: CallConnectionId,
                        cancellationToken: cancellationToken
                        ).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Disconnect the current caller in a group-call or end a p2p-call. </summary>
        /// <param name="forEveryone"> If true, this will terminate the call and hang up on all participants in this call. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        public virtual Response HangUp(bool forEveryone, CancellationToken cancellationToken = default)
        {
            HangUpOptions options = new HangUpOptions(forEveryone);

            return HangUp(options, cancellationToken);
        }

        /// <summary> Disconnect the current caller in a group-call or end a p2p-call. </summary>
        /// <param name="options"> Options for the HangUp call operation. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual Response HangUp(HangUpOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(HangUp)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                if (options.ForEveryone)
                {
                    options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                    return RestClient.TerminateCall(
                        CallConnectionId,
                        options.RepeatabilityHeaders?.RepeatabilityRequestId,
                        options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                        cancellationToken
                        );
                }
                else
                {
                    return RestClient.HangupCall(
                        callConnectionId: CallConnectionId,
                        cancellationToken: cancellationToken
                        );
                }
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Transfer this call to a participant. </summary>
        /// <param name="options"> Options for the Transfer Call To Participant operation. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ArgumentNullException"> SourceCallerId is null in <paramref name="options"/> when transferring the call to a PSTN target.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual async Task<Response<TransferCallToParticipantResult>> TransferCallToParticipantAsync(TransferToParticipantOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(TransferCallToParticipant)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                TransferToParticipantRequestInternal request = CreateTransferToParticipantRequest(options);
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();

                var response = await RestClient.TransferToParticipantAsync(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders?.RepeatabilityRequestId,
                    options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                    cancellationToken
                    ).ConfigureAwait(false);

                var result = response.Value;
                result.SetEventProcessor(EventProcessor, CallConnectionId, result.OperationContext);

                return Response.FromValue(result, response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Transfer the call. </summary>
        /// <param name="options"> Options for the Transfer Call To Participant operation. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ArgumentNullException"> SourceCallerId is null in <paramref name="options"/> when transferring the call to a PSTN target.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual Response<TransferCallToParticipantResult> TransferCallToParticipant(TransferToParticipantOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(TransferCallToParticipant)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                TransferToParticipantRequestInternal request = CreateTransferToParticipantRequest(options);
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();

                var response = RestClient.TransferToParticipant(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders?.RepeatabilityRequestId,
                    options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                    cancellationToken
                    );

                var result = response.Value;
                result.SetEventProcessor(EventProcessor, CallConnectionId, result.OperationContext);

                return Response.FromValue(result, response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        private static TransferToParticipantRequestInternal CreateTransferToParticipantRequest(TransferToParticipantOptions options)
        {
            // when transfer to a PSTN participant, the SourceCallerId must be provided.
            if (options.TargetParticipant is PhoneNumberIdentifier)
            {
                Argument.AssertNotNull(options.SourceCallerId, nameof(options.SourceCallerId));
            }

            TransferToParticipantRequestInternal request = new TransferToParticipantRequestInternal(CommunicationIdentifierSerializer.Serialize(options.TargetParticipant));

            request.TransfereeCallerId = options.SourceCallerId == null ? null : new PhoneNumberIdentifierModel(options.SourceCallerId.PhoneNumber);
            if (options.UserToUserInformation != null && options.UserToUserInformation.Length > CallAutomationConstants.InputValidation.StringMaxLength)
            {
                throw new ArgumentException(CallAutomationErrorMessages.UserToUserInformationExceedsMaxLength);
            }
            else
            {
                request.UserToUserInformation = options.UserToUserInformation;
            }
            if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
            {
                throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
            }
            else
            {
                request.OperationContext = options.OperationContext == default ? Guid.NewGuid().ToString() : options.OperationContext;
            }

            return request;
        }

        /// <summary> Add participants to the call. </summary>
        /// <param name="options"> Options for the Add Participants operation.</param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"> <paramref name="options"/> is null. </exception>
        /// <exception cref="ArgumentNullException"> SourceCallerId is null in <paramref name="options"/> when adding PSTN participants.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual async Task<Response<AddParticipantsResult>> AddParticipantsAsync(AddParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(AddParticipants)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                AddParticipantsRequestInternal request = CreateAddParticipantRequest(options);
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();

                var response = await RestClient.AddParticipantAsync(
                    callConnectionId: CallConnectionId,
                    request,
                    options.RepeatabilityHeaders?.RepeatabilityRequestId,
                    options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                    cancellationToken: cancellationToken
                    ).ConfigureAwait(false);

                var result = new AddParticipantsResult(response);
                result.SetEventProcessor(EventProcessor, CallConnectionId, result.OperationContext);

                return Response.FromValue(result, response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Add participants to the call. </summary>
        /// <param name="options"> Options for the Add Participants operation.</param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"> <paramref name="options"/> is null. </exception>
        /// <exception cref="ArgumentNullException"> SourceCallerId is null in <paramref name="options"/> when adding PSTN participants.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual Response<AddParticipantsResult> AddParticipants(AddParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(AddParticipants)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                AddParticipantsRequestInternal request = CreateAddParticipantRequest(options);
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();

                var response = RestClient.AddParticipant(
                    callConnectionId: CallConnectionId,
                    request,
                    options.RepeatabilityHeaders?.RepeatabilityRequestId,
                    options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                    cancellationToken: cancellationToken
                    );

                var result = new AddParticipantsResult(response);
                result.SetEventProcessor(EventProcessor, CallConnectionId, result.OperationContext);

                return Response.FromValue(result, response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        private static AddParticipantsRequestInternal CreateAddParticipantRequest(AddParticipantsOptions options)
        {
            // when add PSTN participants, the SourceCallerId must be provided.
            if (options.ParticipantsToAdd.Any(participant => participant is PhoneNumberIdentifier))
            {
                Argument.AssertNotNull(options.SourceCallerId, nameof(options.SourceCallerId));
            }

            // validate ParticipantsToAdd is not null or empty
            Argument.AssertNotNullOrEmpty(options.ParticipantsToAdd, nameof(options.ParticipantsToAdd));

            AddParticipantsRequestInternal request = new AddParticipantsRequestInternal(options.ParticipantsToAdd.Select(t => CommunicationIdentifierSerializer.Serialize(t)).ToList());

            request.SourceCallerId = options.SourceCallerId == null ? null : new PhoneNumberIdentifierModel(options.SourceCallerId.PhoneNumber);
            request.SourceDisplayName = options.SourceDisplayName;
            request.SourceIdentifier = options.SourceIdentifier != null ? CommunicationIdentifierSerializer.Serialize(options.SourceIdentifier) : null;
            request.OperationContext = options.OperationContext == default ? Guid.NewGuid().ToString() : options.OperationContext;

            if (options.InvitationTimeoutInSeconds != null &&
                (options.InvitationTimeoutInSeconds < CallAutomationConstants.InputValidation.MinInvitationTimeoutInSeconds ||
                options.InvitationTimeoutInSeconds > CallAutomationConstants.InputValidation.MaxInvitationTimeoutInSeconds))
            {
                throw new ArgumentException(CallAutomationErrorMessages.InvalidInvitationTimeoutInSeconds);
            }
            else
            {
                request.InvitationTimeoutInSeconds = options.InvitationTimeoutInSeconds;
            }

            return request;
        }

        /// <summary> Get participant from a call. </summary>
        /// <param name="participantMri">The participant's MRI.</param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <returns>The <see cref="CallParticipant"/>.</returns>
        public virtual async Task<Response<CallParticipant>> GetParticipantAsync(string participantMri, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(GetParticipant)}");
            scope.Start();
            try
            {
                var response = await RestClient.GetParticipantAsync(
                    callConnectionId: CallConnectionId,
                    participantMri,
                    cancellationToken: cancellationToken
                    ).ConfigureAwait(false);

                return Response.FromValue(new CallParticipant(response.Value), response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Get participant from a call. </summary>
        /// <param name="participantMri">The participant MRI.</param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <returns>The <see cref="CallParticipant"/>.</returns>
        public virtual Response<CallParticipant> GetParticipant(string participantMri, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(GetParticipant)}");
            scope.Start();
            try
            {
                var response = RestClient.GetParticipant(
                    callConnectionId: CallConnectionId,
                    participantMri,
                    cancellationToken: cancellationToken
                    );

                return Response.FromValue(new CallParticipant(response.Value), response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Get participants from a call. </summary>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <returns>The <see cref="IEnumerable{CallParticipant}"/>.</returns>
        public virtual async Task<Response<IReadOnlyList<CallParticipant>>> GetParticipantsAsync(CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(GetParticipants)}");
            scope.Start();
            try
            {
                var response = await RestClient.GetParticipantsAsync(
                    callConnectionId: CallConnectionId,
                    cancellationToken: cancellationToken
                    ).ConfigureAwait(false);

                IReadOnlyList<CallParticipant> result = response.Value.Values.Select(t => new CallParticipant(t)).ToList();

                return Response.FromValue(result, response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Get participants from a call. </summary>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <returns>The <see cref="IEnumerable{CallParticipant}"/>.</returns>
        public virtual Response<IReadOnlyList<CallParticipant>> GetParticipants(CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(GetParticipants)}");
            scope.Start();
            try
            {
                var response = RestClient.GetParticipants(
                    callConnectionId: CallConnectionId,
                    cancellationToken: cancellationToken
                    );

                IReadOnlyList<CallParticipant> result = response.Value.Values.Select(t => new CallParticipant(t)).ToList();

                return Response.FromValue(result, response.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Remove participants from the call. </summary>
        /// <param name="participantsToRemove"> The list of identity of participants to be removed from the call. </param>
        /// <param name="operationContext"> The Operation Context. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentException"> <paramref name="participantsToRemove"/> is empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="participantsToRemove"/> is null. </exception>
        public virtual async Task<Response<RemoveParticipantsResult>> RemoveParticipantsAsync(IEnumerable<CommunicationIdentifier> participantsToRemove, string operationContext = default, CancellationToken cancellationToken = default)
        {
            RemoveParticipantsOptions options = new RemoveParticipantsOptions(participantsToRemove);

            options.OperationContext = operationContext == default ? Guid.NewGuid().ToString() : operationContext;

            return await RemoveParticipantsAsync(options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Remove participants from the call. </summary>
        /// <param name="options">Options for the RemoveParticipants operations.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual async Task<Response<RemoveParticipantsResult>> RemoveParticipantsAsync(RemoveParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(RemoveParticipants)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                // validate RequestInitiator is not null or empty
                Argument.AssertNotNullOrEmpty(options.ParticipantsToRemove, nameof(options.ParticipantsToRemove));

                RemoveParticipantsRequestInternal request = new RemoveParticipantsRequestInternal(options.ParticipantsToRemove.Select(t => CommunicationIdentifierSerializer.Serialize(t)).ToList());
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext == default ? Guid.NewGuid().ToString() : options.OperationContext;
                }

                return await RestClient.RemoveParticipantsAsync(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders?.RepeatabilityRequestId,
                    options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                    cancellationToken
                    ).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Remove participants from the call. </summary>
        /// <param name="participantsToRemove"> The list of identity of participants to be removed from the call. </param>
        /// <param name="operationContext"> The Operation Context. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentException"> <paramref name="participantsToRemove"/> is empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="participantsToRemove"/> is null. </exception>
        public virtual Response<RemoveParticipantsResult> RemoveParticipants(IEnumerable<CommunicationIdentifier> participantsToRemove, string operationContext = default, CancellationToken cancellationToken = default)
        {
            RemoveParticipantsOptions options = new RemoveParticipantsOptions(participantsToRemove);

            options.OperationContext = operationContext == default ? Guid.NewGuid().ToString() : operationContext;

            return RemoveParticipants(options, cancellationToken);
        }

        /// <summary> Remove participants from the call. </summary>
        /// <param name="options">Options for the RemoveParticipants operations.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <exception cref="RequestFailedException">The server returned an error. See <see cref="Exception.Message"/> for details returned from the server.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="options"/> Repeatability headers are set incorrectly.</exception>
        public virtual Response<RemoveParticipantsResult> RemoveParticipants(RemoveParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(RemoveParticipants)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                RemoveParticipantsRequestInternal request = new RemoveParticipantsRequestInternal(options.ParticipantsToRemove.Select(t => CommunicationIdentifierSerializer.Serialize(t)).ToList());
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();

                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    options.OperationContext = options.OperationContext == default ? Guid.NewGuid().ToString() : options.OperationContext;
                }

                return RestClient.RemoveParticipants(
                     CallConnectionId,
                     request,
                     options.RepeatabilityHeaders?.RepeatabilityRequestId,
                     options.RepeatabilityHeaders?.GetRepeatabilityFirstSentString(),
                     cancellationToken
                     );
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary> Initializes a new instance of CallContent. <see cref="CallMedia"/>.</summary>
        public virtual CallMedia GetCallMedia()
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(GetCallMedia)}");
            scope.Start();
            try
            {
                return new CallMedia(CallConnectionId, CallMediaRestClient, _clientDiagnostics, EventProcessor);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Mute a single participant from the call.
        /// </summary>
        /// <param name="targetParticipant">Participant to mute.</param>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Response containing MuteParticipantsResponse.</returns>
        public virtual Response<MuteParticipantsResponse> MuteParticipant(CommunicationIdentifier targetParticipant, string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new MuteParticipantOptions(targetParticipant)
            {
                OperationContext = operationContext
            };

            return MuteParticipant(options, cancellationToken);
        }

        /// <summary>
        /// Mute a single participant from the call.
        /// </summary>
        /// <param name="options">Options for the MuteParticipant operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Response containing MuteParticipantsResponse. </returns>
        public virtual Response<MuteParticipantsResponse> MuteParticipant(MuteParticipantOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(MuteParticipant)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                MuteParticipantRequestInternal request = new MuteParticipantRequestInternal(CommunicationIdentifierSerializer.Serialize(options.TargetParticipant));
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();

                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                return RestClient.Mute(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Unmute a single participant from the call.
        /// </summary>
        /// <param name="targetParticipant">Participant to unmute.</param>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual Response<UnmuteParticipantsResponse> UnmuteParticipant(CommunicationIdentifier targetParticipant, string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new UnmuteParticipantOptions(targetParticipant)
            {
                OperationContext = operationContext,
            };
            return UnmuteParticipant(options, cancellationToken);
        }

        /// <summary>
        /// Unmute a single participant from the call.
        /// </summary>
        /// <param name="options">Options for the UnmuteParticipant operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual Response<UnmuteParticipantsResponse> UnmuteParticipant(UnmuteParticipantOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(UnmuteParticipant)}");
            scope.Start();
            try
            {
                UnmuteParticipantRequestInternal request = new UnmuteParticipantRequestInternal(CommunicationIdentifierSerializer.Serialize(options.TargetParticipant));
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                return RestClient.Unmute(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Mute all participants from the call.
        /// </summary>
        /// <param name="initiator">Participant that initiated the request. If passed, this participant won't be muted.</param>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual Response<MuteParticipantsResponse> MuteAllParticipants(CommunicationIdentifier initiator = default, string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new MuteAllParticipantsOptions()
            {
                RequestInitiator = initiator,
                OperationContext = operationContext,
            };

            return MuteAllParticipants(options, cancellationToken);
        }

        /// <summary>
        /// Mute all participants on the call.
        /// </summary>
        /// <param name="options">Options for the MuteAllParticipants operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual Response<MuteParticipantsResponse> MuteAllParticipants(MuteAllParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(MuteAllParticipants)}");
            scope.Start();
            try
            {
                MuteAllParticipantsRequestInternal request = new MuteAllParticipantsRequestInternal();
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                if (options.RequestInitiator != null)
                {
                    request.RequestInitiator = CommunicationIdentifierSerializer.Serialize(options.RequestInitiator);
                }

                return RestClient.MuteAll(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Unmute all participants on the call.
        /// </summary>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual Response<UnmuteParticipantsResponse> UnmuteAllParticipants(string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new UnmuteAllParticipantsOptions()
            {
                OperationContext = operationContext,
            };

            return UnmuteAllParticipants(options, cancellationToken);
        }

        /// <summary>
        /// Unmute all participants on the call.
        /// </summary>
        /// <param name="options">Options for the UnmuteAllParticipants operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual Response<UnmuteParticipantsResponse> UnmuteAllParticipants(UnmuteAllParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(UnmuteAllParticipants)}");
            scope.Start();
            try
            {
                var request = new UnmuteAllParticipantsRequestInternal();
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                return RestClient.UnmuteAll(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Mute a single participant on the call.
        /// </summary>
        /// <param name="participant">Participant to mute.</param>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async virtual Task<Response<MuteParticipantsResponse>> MuteParticipantAsync(CommunicationIdentifier participant, string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new MuteParticipantOptions(participant)
            {
                OperationContext = operationContext
            };
            return await MuteParticipantAsync(options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Mute a single participant on the call.
        /// </summary>
        /// <param name="options">Options for the MuteParticipant operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async virtual Task<Response<MuteParticipantsResponse>> MuteParticipantAsync(MuteParticipantOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(MuteParticipant)}");
            scope.Start();
            try
            {
                if (options == null)
                    throw new ArgumentNullException(nameof(options));

                MuteParticipantRequestInternal request = new MuteParticipantRequestInternal(CommunicationIdentifierSerializer.Serialize(options.TargetParticipant));
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();

                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                return await RestClient.MuteAsync(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Unmute a single participant on the call.
        /// </summary>
        /// <param name="participant">Participant to unmute.</param>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async virtual Task<Response<UnmuteParticipantsResponse>> UnmuteParticipantAsync(CommunicationIdentifier participant, string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new UnmuteParticipantOptions(participant)
            {
                OperationContext = operationContext
            };

            return await UnmuteParticipantAsync(options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Unmute a single participant from the call.
        /// </summary>
        /// <param name="options">Options for the UnmuteParticipant operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async virtual Task<Response<UnmuteParticipantsResponse>> UnmuteParticipantAsync(UnmuteParticipantOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(UnmuteParticipant)}");
            scope.Start();
            try
            {
                UnmuteParticipantRequestInternal request = new UnmuteParticipantRequestInternal(CommunicationIdentifierSerializer.Serialize(options.TargetParticipant));
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                return await RestClient.UnmuteAsync(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Mute all participants on the call.
        /// </summary>
        /// <param name="initiator">Participant that initiated the request. If passed, this participant won't be muted.</param>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async virtual Task<Response<MuteParticipantsResponse>> MuteAllParticipantsAsync(CommunicationIdentifier initiator = default, string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new MuteAllParticipantsOptions()
            {
                RequestInitiator = initiator,
                OperationContext = operationContext,
            };

            return await MuteAllParticipantsAsync(options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Mute all participants on the call.
        /// </summary>
        /// <param name="options">Options for the MuteAllParticipants operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async virtual Task<Response<MuteParticipantsResponse>> MuteAllParticipantsAsync(MuteAllParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(MuteAllParticipants)}");
            scope.Start();
            try
            {
                MuteAllParticipantsRequestInternal request = new MuteAllParticipantsRequestInternal();
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                if (options.RequestInitiator != null)
                {
                    request.RequestInitiator = CommunicationIdentifierSerializer.Serialize(options.RequestInitiator);
                }

                return await RestClient.MuteAllAsync(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Unmute all participants on the call.
        /// </summary>
        /// <param name="operationContext">The Operation Context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async virtual Task<Response<UnmuteParticipantsResponse>> UnmuteAllParticipantsAsync(string operationContext = default, CancellationToken cancellationToken = default)
        {
            var options = new UnmuteAllParticipantsOptions()
            {
                OperationContext = operationContext,
            };

            return await UnmuteAllParticipantsAsync(options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Unmute all participants on the call.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<Response<UnmuteParticipantsResponse>> UnmuteAllParticipantsAsync(UnmuteAllParticipantsOptions options, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(CallConnection)}.{nameof(UnmuteAllParticipants)}");
            scope.Start();
            try
            {
                var request = new UnmuteAllParticipantsRequestInternal();
                options.RepeatabilityHeaders?.GenerateIfRepeatabilityHeadersNotProvided();
                if (options.OperationContext != null && options.OperationContext.Length > CallAutomationConstants.InputValidation.StringMaxLength)
                {
                    throw new ArgumentException(CallAutomationErrorMessages.OperationContextExceedsMaxLength);
                }
                else
                {
                    request.OperationContext = options.OperationContext;
                }

                return await RestClient.UnmuteAllAsync(
                    CallConnectionId,
                    request,
                    options.RepeatabilityHeaders.RepeatabilityRequestId,
                    options.RepeatabilityHeaders.GetRepeatabilityFirstSentString(),
                    cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }
    }
}
