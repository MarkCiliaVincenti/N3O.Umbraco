//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v9.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming
var ImportsClient = /** @class */ (function () {
    function ImportsClient(baseUrl, http) {
        this.jsonParseReviver = undefined;
        this.http = http ? http : window;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "https://localhost:6001";
    }
    ImportsClient.prototype.addFileToImport = function (referenceId, req) {
        var _this = this;
        var url_ = this.baseUrl + "/umbraco/backoffice/api/Imports/queued/{referenceId}/files";
        if (referenceId === undefined || referenceId === null)
            throw new Error("The parameter 'referenceId' must be defined.");
        url_ = url_.replace("{referenceId}", encodeURIComponent("" + referenceId));
        url_ = url_.replace(/[?&]$/, "");
        var content_ = JSON.stringify(req);
        var options_ = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            }
        };
        return this.http.fetch(url_, options_).then(function (_response) {
            return _this.processAddFileToImport(_response);
        });
    };
    ImportsClient.prototype.processAddFileToImport = function (response) {
        var _this = this;
        var status = response.status;
        var _headers = {};
        if (response.headers && response.headers.forEach) {
            response.headers.forEach(function (v, k) { return _headers[k] = v; });
        }
        ;
        if (status === 200) {
            return response.text().then(function (_responseText) {
                return;
            });
        }
        else if (status === 400) {
            return response.text().then(function (_responseText) {
                var result400 = null;
                result400 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        }
        else if (status === 500) {
            return response.text().then(function (_responseText) {
                return throwException("A server side error occurred.", status, _responseText, _headers);
            });
        }
        else if (status === 404) {
            return response.text().then(function (_responseText) {
                var result404 = null;
                result404 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            });
        }
        else if (status !== 200 && status !== 204) {
            return response.text().then(function (_responseText) {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve(null);
    };
    ImportsClient.prototype.getLookupDatePatterns = function () {
        var _this = this;
        var url_ = this.baseUrl + "/umbraco/backoffice/api/Imports/lookups/datePatterns";
        url_ = url_.replace(/[?&]$/, "");
        var options_ = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };
        return this.http.fetch(url_, options_).then(function (_response) {
            return _this.processGetLookupDatePatterns(_response);
        });
    };
    ImportsClient.prototype.processGetLookupDatePatterns = function (response) {
        var _this = this;
        var status = response.status;
        var _headers = {};
        if (response.headers && response.headers.forEach) {
            response.headers.forEach(function (v, k) { return _headers[k] = v; });
        }
        ;
        if (status === 200) {
            return response.text().then(function (_responseText) {
                var result200 = null;
                result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return result200;
            });
        }
        else if (status === 400) {
            return response.text().then(function (_responseText) {
                var result400 = null;
                result400 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        }
        else if (status === 500) {
            return response.text().then(function (_responseText) {
                return throwException("A server side error occurred.", status, _responseText, _headers);
            });
        }
        else if (status !== 200 && status !== 204) {
            return response.text().then(function (_responseText) {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve(null);
    };
    ImportsClient.prototype.getTemplate = function (contentType) {
        var _this = this;
        var url_ = this.baseUrl + "/umbraco/backoffice/api/Imports/template/{contentType}";
        if (contentType === undefined || contentType === null)
            throw new Error("The parameter 'contentType' must be defined.");
        url_ = url_.replace("{contentType}", encodeURIComponent("" + contentType));
        url_ = url_.replace(/[?&]$/, "");
        var options_ = {
            method: "GET",
            headers: {}
        };
        return this.http.fetch(url_, options_).then(function (_response) {
            return _this.processGetTemplate(_response);
        });
    };
    ImportsClient.prototype.processGetTemplate = function (response) {
        var _this = this;
        var status = response.status;
        var _headers = {};
        if (response.headers && response.headers.forEach) {
            response.headers.forEach(function (v, k) { return _headers[k] = v; });
        }
        ;
        if (status === 200) {
            return response.text().then(function (_responseText) {
                return;
            });
        }
        else if (status === 400) {
            return response.text().then(function (_responseText) {
                var result400 = null;
                result400 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        }
        else if (status === 500) {
            return response.text().then(function (_responseText) {
                return throwException("A server side error occurred.", status, _responseText, _headers);
            });
        }
        else if (status === 404) {
            return response.text().then(function (_responseText) {
                var result404 = null;
                result404 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            });
        }
        else if (status !== 200 && status !== 204) {
            return response.text().then(function (_responseText) {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve(null);
    };
    ImportsClient.prototype.queue = function (containerId, contentType, req) {
        var _this = this;
        var url_ = this.baseUrl + "/umbraco/backoffice/api/Imports/queue/{containerId}/{contentType}";
        if (containerId === undefined || containerId === null)
            throw new Error("The parameter 'containerId' must be defined.");
        url_ = url_.replace("{containerId}", encodeURIComponent("" + containerId));
        if (contentType === undefined || contentType === null)
            throw new Error("The parameter 'contentType' must be defined.");
        url_ = url_.replace("{contentType}", encodeURIComponent("" + contentType));
        url_ = url_.replace(/[?&]$/, "");
        var content_ = JSON.stringify(req);
        var options_ = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };
        return this.http.fetch(url_, options_).then(function (_response) {
            return _this.processQueue(_response);
        });
    };
    ImportsClient.prototype.processQueue = function (response) {
        var _this = this;
        var status = response.status;
        var _headers = {};
        if (response.headers && response.headers.forEach) {
            response.headers.forEach(function (v, k) { return _headers[k] = v; });
        }
        ;
        if (status === 200) {
            return response.text().then(function (_responseText) {
                var result200 = null;
                result200 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return result200;
            });
        }
        else if (status === 400) {
            return response.text().then(function (_responseText) {
                var result400 = null;
                result400 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        }
        else if (status === 500) {
            return response.text().then(function (_responseText) {
                return throwException("A server side error occurred.", status, _responseText, _headers);
            });
        }
        else if (status === 404) {
            return response.text().then(function (_responseText) {
                var result404 = null;
                result404 = _responseText === "" ? null : JSON.parse(_responseText, _this.jsonParseReviver);
                return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            });
        }
        else if (status !== 200 && status !== 204) {
            return response.text().then(function (_responseText) {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve(null);
    };
    return ImportsClient;
}());
export { ImportsClient };
/** One of 'dmy', 'mdy', 'ymd' */
export var DatePattern;
(function (DatePattern) {
    DatePattern["Dmy"] = "dmy";
    DatePattern["Mdy"] = "mdy";
    DatePattern["Ymd"] = "ymd";
})(DatePattern || (DatePattern = {}));
/** One of 'dmy_slashes', 'dmy_dashes', 'mdy_slashes', 'mdy_dashes', 'ymd_slashes', 'ymd_dashes' */
export var DateFormat;
(function (DateFormat) {
    DateFormat["Dmy_slashes"] = "dmy_slashes";
    DateFormat["Dmy_dashes"] = "dmy_dashes";
    DateFormat["Mdy_slashes"] = "mdy_slashes";
    DateFormat["Mdy_dashes"] = "mdy_dashes";
    DateFormat["Ymd_slashes"] = "ymd_slashes";
    DateFormat["Ymd_dashes"] = "ymd_dashes";
})(DateFormat || (DateFormat = {}));
var ApiException = /** @class */ (function (_super) {
    __extends(ApiException, _super);
    function ApiException(message, status, response, headers, result) {
        var _this = _super.call(this) || this;
        _this.isApiException = true;
        _this.message = message;
        _this.status = status;
        _this.response = response;
        _this.headers = headers;
        _this.result = result;
        return _this;
    }
    ApiException.isApiException = function (obj) {
        return obj.isApiException === true;
    };
    return ApiException;
}(Error));
export { ApiException };
function throwException(message, status, response, headers, result) {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}
//# sourceMappingURL=index.js.map