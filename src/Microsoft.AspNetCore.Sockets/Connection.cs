// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Sockets.Features;

namespace Microsoft.AspNetCore.Sockets
{
    public class Connection
    {
        private static readonly Func<IAuthenticationFeature> _newAuthenticationFeature =
            () => new AuthenticationFeature();

        public string ConnectionId { get; }

        public IFeatureCollection Features { get; }

        public ClaimsPrincipal User
        {
            get => GetOrAddFeature<IAuthenticationFeature>(_newAuthenticationFeature).User;
            set => GetOrAddFeature<IAuthenticationFeature>(_newAuthenticationFeature).User = value;
        }

        public ConnectionMetadata Metadata { get; } = new ConnectionMetadata();

        public IChannelConnection<Message> Transport { get; }

        public Connection(string id, IChannelConnection<Message> transport)
        {
            Transport = transport;
            ConnectionId = id;
            Features = new FeatureCollection();
        }

        private T GetOrAddFeature<T>(Func<T> constructor) where T : class
        {
            var feature = Features.Get<T>();
            if (feature == null)
            {
                feature = constructor();
                Features.Set(feature);
            }
            return feature;
        }
    }
}
