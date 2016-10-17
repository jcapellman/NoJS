﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

using NoJS.Library.Interfaces;

namespace NoJS.Library {
    public class PreferenceSwitcher {
        private readonly IDeviceFactory _deviceFactory;
        private readonly IOptions<SwitcherOptions> _options;
        private readonly ISitePreferenceRepository _repository;

        public PreferenceSwitcher(IOptions<SwitcherOptions> options, ISitePreferenceRepository repository, IDeviceFactory deviceFactory) {
            _options = options;
            _repository = repository;
            _deviceFactory = deviceFactory;
        }

        public virtual Task Handle(HttpContext context) {
            return Task.Run(() => {
                var device = context.GetRouteValue("device").ToString();

                if (string.IsNullOrWhiteSpace(device)) {
                    return;
                }

                if (device == _options.Value.LegacyKey) {
                    _repository.SavePreference(context, _deviceFactory.Legacy());
                } else if (device == _options.Value.NormalKey) {
                    _repository.SavePreference(context, _deviceFactory.Normal());
                }
            });
        }
    }
}