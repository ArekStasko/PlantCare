function _assert_this_initialized(self) {
    if (self === void 0) {
        throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
    }
    return self;
}
function _call_super(_this, derived, args) {
    derived = _get_prototype_of(derived);
    return _possible_constructor_return(_this, _is_native_reflect_construct() ? Reflect.construct(derived, args || [], _get_prototype_of(_this).constructor) : derived.apply(_this, args));
}
function _class_call_check(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
        throw new TypeError("Cannot call a class as a function");
    }
}
function _construct(Parent, args, Class) {
    if (_is_native_reflect_construct()) {
        _construct = Reflect.construct;
    } else {
        _construct = function construct(Parent, args, Class) {
            var a = [
                null
            ];
            a.push.apply(a, args);
            var Constructor = Function.bind.apply(Parent, a);
            var instance = new Constructor();
            if (Class) _set_prototype_of(instance, Class.prototype);
            return instance;
        };
    }
    return _construct.apply(null, arguments);
}
function _defineProperties(target, props) {
    for(var i = 0; i < props.length; i++){
        var descriptor = props[i];
        descriptor.enumerable = descriptor.enumerable || false;
        descriptor.configurable = true;
        if ("value" in descriptor) descriptor.writable = true;
        Object.defineProperty(target, descriptor.key, descriptor);
    }
}
function _create_class(Constructor, protoProps, staticProps) {
    if (protoProps) _defineProperties(Constructor.prototype, protoProps);
    if (staticProps) _defineProperties(Constructor, staticProps);
    return Constructor;
}
function _get_prototype_of(o) {
    _get_prototype_of = Object.setPrototypeOf ? Object.getPrototypeOf : function getPrototypeOf(o) {
        return o.__proto__ || Object.getPrototypeOf(o);
    };
    return _get_prototype_of(o);
}
function _inherits(subClass, superClass) {
    if (typeof superClass !== "function" && superClass !== null) {
        throw new TypeError("Super expression must either be null or a function");
    }
    subClass.prototype = Object.create(superClass && superClass.prototype, {
        constructor: {
            value: subClass,
            writable: true,
            configurable: true
        }
    });
    if (superClass) _set_prototype_of(subClass, superClass);
}
function _is_native_function(fn) {
    return Function.toString.call(fn).indexOf("[native code]") !== -1;
}
function _possible_constructor_return(self, call) {
    if (call && (_type_of(call) === "object" || typeof call === "function")) {
        return call;
    }
    return _assert_this_initialized(self);
}
function _set_prototype_of(o, p) {
    _set_prototype_of = Object.setPrototypeOf || function setPrototypeOf(o, p) {
        o.__proto__ = p;
        return o;
    };
    return _set_prototype_of(o, p);
}
function _type_of(obj) {
    "@swc/helpers - typeof";
    return obj && typeof Symbol !== "undefined" && obj.constructor === Symbol ? "symbol" : typeof obj;
}
function _wrap_native_super(Class) {
    var _cache = typeof Map === "function" ? new Map() : undefined;
    _wrap_native_super = function wrapNativeSuper(Class) {
        if (Class === null || !_is_native_function(Class)) return Class;
        if (typeof Class !== "function") {
            throw new TypeError("Super expression must either be null or a function");
        }
        if (typeof _cache !== "undefined") {
            if (_cache.has(Class)) return _cache.get(Class);
            _cache.set(Class, Wrapper);
        }
        function Wrapper() {
            return _construct(Class, arguments, _get_prototype_of(this).constructor);
        }
        Wrapper.prototype = Object.create(Class.prototype, {
            constructor: {
                value: Wrapper,
                enumerable: false,
                writable: true,
                configurable: true
            }
        });
        return _set_prototype_of(Wrapper, Class);
    };
    return _wrap_native_super(Class);
}
function _is_native_reflect_construct() {
    try {
        var result = !Boolean.prototype.valueOf.call(Reflect.construct(Boolean, [], function() {}));
    } catch (_) {}
    return (_is_native_reflect_construct = function() {
        return !!result;
    })();
}
// ApiClient.ts
var Client = /*#__PURE__*/ function() {
    "use strict";
    function Client(baseUrl, http) {
        _class_call_check(this, Client);
        this.jsonParseReviver = void 0;
        this.http = http ? http : window;
        this.baseUrl = baseUrl !== null && baseUrl !== void 0 ? baseUrl : "";
    }
    _create_class(Client, [
        {
            /**
   * @param body (optional) 
   * @return OK
   */ key: "humidityMeasurements",
            value: function humidityMeasurements(body) {
                var _this = this;
                var url_ = this.baseUrl + "/api/humidity-measurements";
                url_ = url_.replace(/[?&]$/, "");
                var content_ = JSON.stringify(body);
                var options_ = {
                    body: content_,
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processHumidityMeasurements(_response);
                });
            }
        },
        {
            key: "processHumidityMeasurements",
            value: function processHumidityMeasurements(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param id (optional) 
   * @param fromDate (optional) 
   * @param toDate (optional) 
   * @return OK
   */ key: "humidityMeasurementsAll",
            value: function humidityMeasurementsAll(id, fromDate, toDate) {
                var _this = this;
                var url_ = this.baseUrl + "/api/humidity-measurements?";
                if (id === null) throw new globalThis.Error("The parameter 'id' cannot be null.");
                else if (id !== void 0) url_ += "id=" + encodeURIComponent("" + id) + "&";
                if (fromDate === null) throw new globalThis.Error("The parameter 'fromDate' cannot be null.");
                else if (fromDate !== void 0) url_ += "fromDate=" + encodeURIComponent(fromDate ? "" + fromDate.toISOString() : "") + "&";
                if (toDate === null) throw new globalThis.Error("The parameter 'toDate' cannot be null.");
                else if (toDate !== void 0) url_ += "toDate=" + encodeURIComponent(toDate ? "" + toDate.toISOString() : "") + "&";
                url_ = url_.replace(/[?&]$/, "");
                var options_ = {
                    method: "GET",
                    headers: {
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processHumidityMeasurementsAll(_response);
                });
            }
        },
        {
            key: "processHumidityMeasurementsAll",
            value: function processHumidityMeasurementsAll(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param body (optional) 
   * @return OK
   */ key: "status",
            value: function status(body) {
                var _this = this;
                var url_ = this.baseUrl + "/status";
                url_ = url_.replace(/[?&]$/, "");
                var content_ = JSON.stringify(body);
                var options_ = {
                    body: content_,
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processStatus(_response);
                });
            }
        },
        {
            key: "processStatus",
            value: function processStatus(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param body (optional) 
   * @return OK
   */ key: "modules",
            value: function modules(body) {
                var _this = this;
                var url_ = this.baseUrl + "/api/modules";
                url_ = url_.replace(/[?&]$/, "");
                var content_ = JSON.stringify(body);
                var options_ = {
                    body: content_,
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processModules(_response);
                });
            }
        },
        {
            key: "processModules",
            value: function processModules(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @return OK
   */ key: "modulesAll",
            value: function modulesAll() {
                var _this = this;
                var url_ = this.baseUrl + "/api/modules";
                url_ = url_.replace(/[?&]$/, "");
                var options_ = {
                    method: "GET",
                    headers: {
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processModulesAll(_response);
                });
            }
        },
        {
            key: "processModulesAll",
            value: function processModulesAll(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param body (optional) 
   * @return OK
   */ key: "placesPOST",
            value: function placesPOST(body) {
                var _this = this;
                var url_ = this.baseUrl + "/api/places";
                url_ = url_.replace(/[?&]$/, "");
                var content_ = JSON.stringify(body);
                var options_ = {
                    body: content_,
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlacesPOST(_response);
                });
            }
        },
        {
            key: "processPlacesPOST",
            value: function processPlacesPOST(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param id (optional) 
   * @return OK
   */ key: "placesDELETE",
            value: function placesDELETE(id) {
                var _this = this;
                var url_ = this.baseUrl + "/api/places?";
                if (id === null) throw new globalThis.Error("The parameter 'id' cannot be null.");
                else if (id !== void 0) url_ += "id=" + encodeURIComponent("" + id) + "&";
                url_ = url_.replace(/[?&]$/, "");
                var options_ = {
                    method: "DELETE",
                    headers: {
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlacesDELETE(_response);
                });
            }
        },
        {
            key: "processPlacesDELETE",
            value: function processPlacesDELETE(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param body (optional) 
   * @return OK
   */ key: "placesPUT",
            value: function placesPUT(body) {
                var _this = this;
                var url_ = this.baseUrl + "/api/places";
                url_ = url_.replace(/[?&]$/, "");
                var content_ = JSON.stringify(body);
                var options_ = {
                    body: content_,
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlacesPUT(_response);
                });
            }
        },
        {
            key: "processPlacesPUT",
            value: function processPlacesPUT(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @return OK
   */ key: "placesAll",
            value: function placesAll() {
                var _this = this;
                var url_ = this.baseUrl + "/api/places";
                url_ = url_.replace(/[?&]$/, "");
                var options_ = {
                    method: "GET",
                    headers: {
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlacesAll(_response);
                });
            }
        },
        {
            key: "processPlacesAll",
            value: function processPlacesAll(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param body (optional) 
   * @return OK
   */ key: "plantsPOST",
            value: function plantsPOST(body) {
                var _this = this;
                var url_ = this.baseUrl + "/api/plants";
                url_ = url_.replace(/[?&]$/, "");
                var content_ = JSON.stringify(body);
                var options_ = {
                    body: content_,
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlantsPOST(_response);
                });
            }
        },
        {
            key: "processPlantsPOST",
            value: function processPlantsPOST(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param id (optional) 
   * @return OK
   */ key: "plantsDELETE",
            value: function plantsDELETE(id) {
                var _this = this;
                var url_ = this.baseUrl + "/api/plants?";
                if (id === null) throw new globalThis.Error("The parameter 'id' cannot be null.");
                else if (id !== void 0) url_ += "id=" + encodeURIComponent("" + id) + "&";
                url_ = url_.replace(/[?&]$/, "");
                var options_ = {
                    method: "DELETE",
                    headers: {
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlantsDELETE(_response);
                });
            }
        },
        {
            key: "processPlantsDELETE",
            value: function processPlantsDELETE(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @param body (optional) 
   * @return OK
   */ key: "plantsPUT",
            value: function plantsPUT(body) {
                var _this = this;
                var url_ = this.baseUrl + "/api/plants";
                url_ = url_.replace(/[?&]$/, "");
                var content_ = JSON.stringify(body);
                var options_ = {
                    body: content_,
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlantsPUT(_response);
                });
            }
        },
        {
            key: "processPlantsPUT",
            value: function processPlantsPUT(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @return OK
   */ key: "plantsAll",
            value: function plantsAll() {
                var _this = this;
                var url_ = this.baseUrl + "/api/plants";
                url_ = url_.replace(/[?&]$/, "");
                var options_ = {
                    method: "GET",
                    headers: {
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processPlantsAll(_response);
                });
            }
        },
        {
            key: "processPlantsAll",
            value: function processPlantsAll(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        },
        {
            /**
   * @return OK
   */ key: "anonymous",
            value: function anonymous(id) {
                var _this = this;
                var url_ = this.baseUrl + "/{id}";
                if (id === void 0 || id === null) throw new globalThis.Error("The parameter 'id' must be defined.");
                url_ = url_.replace("{id}", encodeURIComponent("" + id));
                url_ = url_.replace(/[?&]$/, "");
                var options_ = {
                    method: "GET",
                    headers: {
                        "Accept": "application/json"
                    }
                };
                return this.http.fetch(url_, options_).then(function(_response) {
                    return _this.processAnonymous(_response);
                });
            }
        },
        {
            key: "processAnonymous",
            value: function processAnonymous(response) {
                var _this = this;
                var status = response.status;
                var _headers = {};
                if (response.headers && response.headers.forEach) {
                    response.headers.forEach(function(v, k) {
                        return _headers[k] = v;
                    });
                }
                ;
                if (status === 200) {
                    return response.text().then(function(_responseText) {
                        var result200 = null;
                        result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return result200;
                    });
                } else if (status === 500) {
                    return response.text().then(function(_responseText) {
                        var result500 = null;
                        result500 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                        return throwException("Internal Server Error", status, _responseText, _headers, result500);
                    });
                } else if (status !== 200 && status !== 204) {
                    return response.text().then(function(_responseText) {
                        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
                    });
                }
                return Promise.resolve(null);
            }
        }
    ]);
    return Client;
}();
var CallingConventions = /* @__PURE__ */ function(CallingConventions2) {
    CallingConventions2[CallingConventions2["_1"] = 1] = "_1";
    CallingConventions2[CallingConventions2["_2"] = 2] = "_2";
    CallingConventions2[CallingConventions2["_3"] = 3] = "_3";
    CallingConventions2[CallingConventions2["_32"] = 32] = "_32";
    CallingConventions2[CallingConventions2["_64"] = 64] = "_64";
    return CallingConventions2;
}(CallingConventions || {});
var EventAttributes = /* @__PURE__ */ function(EventAttributes2) {
    EventAttributes2[EventAttributes2["_0"] = 0] = "_0";
    EventAttributes2[EventAttributes2["_512"] = 512] = "_512";
    EventAttributes2[EventAttributes2["_1024"] = 1024] = "_1024";
    return EventAttributes2;
}(EventAttributes || {});
var FieldAttributes = /* @__PURE__ */ function(FieldAttributes2) {
    FieldAttributes2[FieldAttributes2["_0"] = 0] = "_0";
    FieldAttributes2[FieldAttributes2["_1"] = 1] = "_1";
    FieldAttributes2[FieldAttributes2["_2"] = 2] = "_2";
    FieldAttributes2[FieldAttributes2["_3"] = 3] = "_3";
    FieldAttributes2[FieldAttributes2["_4"] = 4] = "_4";
    FieldAttributes2[FieldAttributes2["_5"] = 5] = "_5";
    FieldAttributes2[FieldAttributes2["_6"] = 6] = "_6";
    FieldAttributes2[FieldAttributes2["_7"] = 7] = "_7";
    FieldAttributes2[FieldAttributes2["_16"] = 16] = "_16";
    FieldAttributes2[FieldAttributes2["_32"] = 32] = "_32";
    FieldAttributes2[FieldAttributes2["_64"] = 64] = "_64";
    FieldAttributes2[FieldAttributes2["_128"] = 128] = "_128";
    FieldAttributes2[FieldAttributes2["_256"] = 256] = "_256";
    FieldAttributes2[FieldAttributes2["_512"] = 512] = "_512";
    FieldAttributes2[FieldAttributes2["_1024"] = 1024] = "_1024";
    FieldAttributes2[FieldAttributes2["_4096"] = 4096] = "_4096";
    FieldAttributes2[FieldAttributes2["_8192"] = 8192] = "_8192";
    FieldAttributes2[FieldAttributes2["_32768"] = 32768] = "_32768";
    FieldAttributes2[FieldAttributes2["_38144"] = 38144] = "_38144";
    return FieldAttributes2;
}(FieldAttributes || {});
var GenericParameterAttributes = /* @__PURE__ */ function(GenericParameterAttributes2) {
    GenericParameterAttributes2[GenericParameterAttributes2["_0"] = 0] = "_0";
    GenericParameterAttributes2[GenericParameterAttributes2["_1"] = 1] = "_1";
    GenericParameterAttributes2[GenericParameterAttributes2["_2"] = 2] = "_2";
    GenericParameterAttributes2[GenericParameterAttributes2["_3"] = 3] = "_3";
    GenericParameterAttributes2[GenericParameterAttributes2["_4"] = 4] = "_4";
    GenericParameterAttributes2[GenericParameterAttributes2["_8"] = 8] = "_8";
    GenericParameterAttributes2[GenericParameterAttributes2["_16"] = 16] = "_16";
    GenericParameterAttributes2[GenericParameterAttributes2["_28"] = 28] = "_28";
    return GenericParameterAttributes2;
}(GenericParameterAttributes || {});
var LayoutKind = /* @__PURE__ */ function(LayoutKind2) {
    LayoutKind2[LayoutKind2["_0"] = 0] = "_0";
    LayoutKind2[LayoutKind2["_2"] = 2] = "_2";
    LayoutKind2[LayoutKind2["_3"] = 3] = "_3";
    return LayoutKind2;
}(LayoutKind || {});
var MemberTypes = /* @__PURE__ */ function(MemberTypes2) {
    MemberTypes2[MemberTypes2["_1"] = 1] = "_1";
    MemberTypes2[MemberTypes2["_2"] = 2] = "_2";
    MemberTypes2[MemberTypes2["_4"] = 4] = "_4";
    MemberTypes2[MemberTypes2["_8"] = 8] = "_8";
    MemberTypes2[MemberTypes2["_16"] = 16] = "_16";
    MemberTypes2[MemberTypes2["_32"] = 32] = "_32";
    MemberTypes2[MemberTypes2["_64"] = 64] = "_64";
    MemberTypes2[MemberTypes2["_128"] = 128] = "_128";
    MemberTypes2[MemberTypes2["_191"] = 191] = "_191";
    return MemberTypes2;
}(MemberTypes || {});
var MethodAttributes = /* @__PURE__ */ function(MethodAttributes2) {
    MethodAttributes2[MethodAttributes2["_0"] = 0] = "_0";
    MethodAttributes2[MethodAttributes2["_1"] = 1] = "_1";
    MethodAttributes2[MethodAttributes2["_2"] = 2] = "_2";
    MethodAttributes2[MethodAttributes2["_3"] = 3] = "_3";
    MethodAttributes2[MethodAttributes2["_4"] = 4] = "_4";
    MethodAttributes2[MethodAttributes2["_5"] = 5] = "_5";
    MethodAttributes2[MethodAttributes2["_6"] = 6] = "_6";
    MethodAttributes2[MethodAttributes2["_7"] = 7] = "_7";
    MethodAttributes2[MethodAttributes2["_8"] = 8] = "_8";
    MethodAttributes2[MethodAttributes2["_16"] = 16] = "_16";
    MethodAttributes2[MethodAttributes2["_32"] = 32] = "_32";
    MethodAttributes2[MethodAttributes2["_64"] = 64] = "_64";
    MethodAttributes2[MethodAttributes2["_128"] = 128] = "_128";
    MethodAttributes2[MethodAttributes2["_256"] = 256] = "_256";
    MethodAttributes2[MethodAttributes2["_512"] = 512] = "_512";
    MethodAttributes2[MethodAttributes2["_1024"] = 1024] = "_1024";
    MethodAttributes2[MethodAttributes2["_2048"] = 2048] = "_2048";
    MethodAttributes2[MethodAttributes2["_4096"] = 4096] = "_4096";
    MethodAttributes2[MethodAttributes2["_8192"] = 8192] = "_8192";
    MethodAttributes2[MethodAttributes2["_16384"] = 16384] = "_16384";
    MethodAttributes2[MethodAttributes2["_32768"] = 32768] = "_32768";
    MethodAttributes2[MethodAttributes2["_53248"] = 53248] = "_53248";
    return MethodAttributes2;
}(MethodAttributes || {});
var MethodImplAttributes = /* @__PURE__ */ function(MethodImplAttributes2) {
    MethodImplAttributes2[MethodImplAttributes2["_0"] = 0] = "_0";
    MethodImplAttributes2[MethodImplAttributes2["_1"] = 1] = "_1";
    MethodImplAttributes2[MethodImplAttributes2["_2"] = 2] = "_2";
    MethodImplAttributes2[MethodImplAttributes2["_3"] = 3] = "_3";
    MethodImplAttributes2[MethodImplAttributes2["_4"] = 4] = "_4";
    MethodImplAttributes2[MethodImplAttributes2["_8"] = 8] = "_8";
    MethodImplAttributes2[MethodImplAttributes2["_16"] = 16] = "_16";
    MethodImplAttributes2[MethodImplAttributes2["_32"] = 32] = "_32";
    MethodImplAttributes2[MethodImplAttributes2["_64"] = 64] = "_64";
    MethodImplAttributes2[MethodImplAttributes2["_128"] = 128] = "_128";
    MethodImplAttributes2[MethodImplAttributes2["_256"] = 256] = "_256";
    MethodImplAttributes2[MethodImplAttributes2["_512"] = 512] = "_512";
    MethodImplAttributes2[MethodImplAttributes2["_4096"] = 4096] = "_4096";
    MethodImplAttributes2[MethodImplAttributes2["_65535"] = 65535] = "_65535";
    return MethodImplAttributes2;
}(MethodImplAttributes || {});
var ParameterAttributes = /* @__PURE__ */ function(ParameterAttributes2) {
    ParameterAttributes2[ParameterAttributes2["_0"] = 0] = "_0";
    ParameterAttributes2[ParameterAttributes2["_1"] = 1] = "_1";
    ParameterAttributes2[ParameterAttributes2["_2"] = 2] = "_2";
    ParameterAttributes2[ParameterAttributes2["_4"] = 4] = "_4";
    ParameterAttributes2[ParameterAttributes2["_8"] = 8] = "_8";
    ParameterAttributes2[ParameterAttributes2["_16"] = 16] = "_16";
    ParameterAttributes2[ParameterAttributes2["_4096"] = 4096] = "_4096";
    ParameterAttributes2[ParameterAttributes2["_8192"] = 8192] = "_8192";
    ParameterAttributes2[ParameterAttributes2["_16384"] = 16384] = "_16384";
    ParameterAttributes2[ParameterAttributes2["_32768"] = 32768] = "_32768";
    ParameterAttributes2[ParameterAttributes2["_61440"] = 61440] = "_61440";
    return ParameterAttributes2;
}(ParameterAttributes || {});
var PlantType = /* @__PURE__ */ function(PlantType2) {
    PlantType2[PlantType2["_0"] = 0] = "_0";
    PlantType2[PlantType2["_1"] = 1] = "_1";
    PlantType2[PlantType2["_2"] = 2] = "_2";
    return PlantType2;
}(PlantType || {});
var PropertyAttributes = /* @__PURE__ */ function(PropertyAttributes2) {
    PropertyAttributes2[PropertyAttributes2["_0"] = 0] = "_0";
    PropertyAttributes2[PropertyAttributes2["_512"] = 512] = "_512";
    PropertyAttributes2[PropertyAttributes2["_1024"] = 1024] = "_1024";
    PropertyAttributes2[PropertyAttributes2["_4096"] = 4096] = "_4096";
    PropertyAttributes2[PropertyAttributes2["_8192"] = 8192] = "_8192";
    PropertyAttributes2[PropertyAttributes2["_16384"] = 16384] = "_16384";
    PropertyAttributes2[PropertyAttributes2["_32768"] = 32768] = "_32768";
    PropertyAttributes2[PropertyAttributes2["_62464"] = 62464] = "_62464";
    return PropertyAttributes2;
}(PropertyAttributes || {});
var SecurityRuleSet = /* @__PURE__ */ function(SecurityRuleSet2) {
    SecurityRuleSet2[SecurityRuleSet2["_0"] = 0] = "_0";
    SecurityRuleSet2[SecurityRuleSet2["_1"] = 1] = "_1";
    SecurityRuleSet2[SecurityRuleSet2["_2"] = 2] = "_2";
    return SecurityRuleSet2;
}(SecurityRuleSet || {});
var TypeAttributes = /* @__PURE__ */ function(TypeAttributes2) {
    TypeAttributes2[TypeAttributes2["_0"] = 0] = "_0";
    TypeAttributes2[TypeAttributes2["_1"] = 1] = "_1";
    TypeAttributes2[TypeAttributes2["_2"] = 2] = "_2";
    TypeAttributes2[TypeAttributes2["_3"] = 3] = "_3";
    TypeAttributes2[TypeAttributes2["_4"] = 4] = "_4";
    TypeAttributes2[TypeAttributes2["_5"] = 5] = "_5";
    TypeAttributes2[TypeAttributes2["_6"] = 6] = "_6";
    TypeAttributes2[TypeAttributes2["_7"] = 7] = "_7";
    TypeAttributes2[TypeAttributes2["_8"] = 8] = "_8";
    TypeAttributes2[TypeAttributes2["_16"] = 16] = "_16";
    TypeAttributes2[TypeAttributes2["_24"] = 24] = "_24";
    TypeAttributes2[TypeAttributes2["_32"] = 32] = "_32";
    TypeAttributes2[TypeAttributes2["_128"] = 128] = "_128";
    TypeAttributes2[TypeAttributes2["_256"] = 256] = "_256";
    TypeAttributes2[TypeAttributes2["_1024"] = 1024] = "_1024";
    TypeAttributes2[TypeAttributes2["_2048"] = 2048] = "_2048";
    TypeAttributes2[TypeAttributes2["_4096"] = 4096] = "_4096";
    TypeAttributes2[TypeAttributes2["_8192"] = 8192] = "_8192";
    TypeAttributes2[TypeAttributes2["_16384"] = 16384] = "_16384";
    TypeAttributes2[TypeAttributes2["_65536"] = 65536] = "_65536";
    TypeAttributes2[TypeAttributes2["_131072"] = 131072] = "_131072";
    TypeAttributes2[TypeAttributes2["_196608"] = 196608] = "_196608";
    TypeAttributes2[TypeAttributes2["_262144"] = 262144] = "_262144";
    TypeAttributes2[TypeAttributes2["_264192"] = 264192] = "_264192";
    TypeAttributes2[TypeAttributes2["_1048576"] = 1048576] = "_1048576";
    TypeAttributes2[TypeAttributes2["_12582912"] = 12582912] = "_12582912";
    return TypeAttributes2;
}(TypeAttributes || {});
var ApiException = /*#__PURE__*/ function(Error1) {
    "use strict";
    _inherits(ApiException, Error1);
    function ApiException(message, status, response, headers, result) {
        _class_call_check(this, ApiException);
        var _this;
        _this = _call_super(this, ApiException);
        _this.isApiException = true;
        _this.message = message;
        _this.status = status;
        _this.response = response;
        _this.headers = headers;
        _this.result = result;
        return _this;
    }
    _create_class(ApiException, null, [
        {
            key: "isApiException",
            value: function isApiException(obj) {
                return obj.isApiException === true;
            }
        }
    ]);
    return ApiException;
}(_wrap_native_super(Error));
function throwException(message, status, response, headers, result) {
    if (result !== null && result !== void 0) throw result;
    else throw new ApiException(message, status, response, headers, null);
}
export { ApiException, CallingConventions, Client, EventAttributes, FieldAttributes, GenericParameterAttributes, LayoutKind, MemberTypes, MethodAttributes, MethodImplAttributes, ParameterAttributes, PlantType, PropertyAttributes, SecurityRuleSet, TypeAttributes };
