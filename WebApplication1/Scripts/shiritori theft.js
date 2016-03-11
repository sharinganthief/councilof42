function getSearchParameters() {
    var e = window.location.search.substr(1);
    return e != null && e != "" ? transformToAssocArray(e) : {}
}

function transformToAssocArray(e) {
    var t = {},
        n = e.split("&");
    for (var r = 0; r < n.length; r++) {
        var i = n[r].split("=");
        t[i[0]] = i[1]
    }
    return t
}

function(e) {
    function t(e, t) {
        return e.toFixed(t.decimals)
    }
    e.fn.countTo = function(t) {
        return t = t || {}, e(this).each(function() {
            function l() {
                a += i, u++, c(a), typeof n.onUpdate == "function" && n.onUpdate.call(s, a), u >= r && (o.removeData("countTo"), clearInterval(f.interval), a = n.to, typeof n.onComplete == "function" && n.onComplete.call(s, a))
            }

            function c(e) {
                var t = n.formatter.call(s, e, n);
                o.html(t)
            }
            var n = e.extend({}, e.fn.countTo.defaults, {
                from: e(this).data("from"),
                to: e(this).data("to"),
                speed: e(this).data("speed"),
                refreshInterval: e(this).data("refresh-interval"),
                decimals: e(this).data("decimals")
            }, t),
                r = Math.ceil(n.speed / n.refreshInterval),
                i = (n.to - n.from) / r,
                s = this,
                o = e(this),
                u = 0,
                a = n.from,
                f = o.data("countTo") || {};
            o.data("countTo", f), f.interval && clearInterval(f.interval), f.interval = setInterval(l, n.refreshInterval), c(a)
        })
    }, e.fn.countTo.defaults = {
        from: 0,
        to: 0,
        speed: 1e3,
        refreshInterval: 100,
        decimals: 0,
        formatter: t,
        onUpdate: null,
        onComplete: null
    }
}(jQuery), ! function(e) {
    "function" == typeof define && define.amd ? define(["jquery"], e) : "object" == typeof exports ? e(require("jquery")) : e(jQuery)
}(function(e) {
    function t(e) {
        return u.raw ? e : encodeURIComponent(e)
    }

    function n(e) {
        return u.raw ? e : decodeURIComponent(e)
    }

    function r(e) {
        return t(u.json ? JSON.stringify(e) : String(e))
    }

    function i(e) {
        0 === e.indexOf('"') && (e = e.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, "\\"));
        try {
            return e = decodeURIComponent(e.replace(o, " ")), u.json ? JSON.parse(e) : e
        } catch (t) {}
    }

    function s(t, n) {
        var r = u.raw ? t : i(t);
        return e.isFunction(n) ? n(r) : r
    }
    var o = /\+/g,
        u = e.cookie = function(i, o, l) {
            if (void 0 !== o && !e.isFunction(o)) {
                if (l = e.extend({}, u.defaults, l), "number" == typeof l.expires) {
                    var p = l.expires,
                        v = l.expires = new Date;
                    v.setTime(+v + 864e5 * p)
                }
                return document.cookie = [t(i), "=", r(o), l.expires ? "; expires=" + l.expires.toUTCString() : "", l.path ? "; path=" + l.path : "", l.domain ? "; domain=" + l.domain : "", l.secure ? "; secure" : ""].join("")
            }
            for (var m = i ? void 0 : {}, g = document.cookie ? document.cookie.split("; ") : [], y = 0, w = g.length; w > y; y++) {
                var E = g[y].split("="),
                    S = n(E.shift()),
                    x = E.join("=");
                if (i && i === S) {
                    m = s(x, o);
                    break
                }
                i || void 0 === (x = s(x)) || (m[S] = x)
            }
            return m
        };
    u.defaults = {}, e.removeCookie = function(t, n) {
        return void 0 === e.cookie(t) ? !1 : (e.cookie(t, "", e.extend({}, n, {
            expires: -1
        })), !e.cookie(t))
    }
}),
function(e, t) {
    typeof exports == "object" ? t(exports) : typeof define == "function" && define.amd ? define(["exports"], t) : t(e)
}(this, function(e) {
    function n(e) {
        this._targetElement = e, this._options = {
            nextLabel: "Next &rarr;",
            prevLabel: "&larr; Back",
            skipLabel: "Skip",
            doneLabel: "Done",
            tooltipPosition: "bottom",
            tooltipClass: "",
            exitOnEsc: !0,
            exitOnOverlayClick: !0,
            showStepNumbers: !0,
            keyboardNavigation: !0,
            showButtons: !0,
            showBullets: !0,
            scrollToElement: !0
        }
    }

    function r(e) {
        var t = [],
            n = this;
        if (this._options.steps) {
            var r = [];
            for (var s = 0, f = this._options.steps.length; s < f; s++) {
                var c = i(this._options.steps[s]);
                c.step = t.length + 1, typeof c.element == "string" && (c.element = document.querySelector(c.element));
                if (typeof c.element == "undefined" || c.element == null) {
                    var h = document.querySelector(".introjsFloatingElement");
                    h == null && (h = document.createElement("div"), h.className = "introjsFloatingElement", document.body.appendChild(h)), c.element = h, c.position = "floating"
                }
                c.element != null && t.push(c)
            }
        } else {
            var r = e.querySelectorAll("*[data-intro]");
            if (r.length < 1) return !1;
            for (var s = 0, p = r.length; s < p; s++) {
                var d = r[s],
                    m = parseInt(d.getAttribute("data-step"), 10);
                m > 0 && (t[m - 1] = {
                    element: d,
                    intro: d.getAttribute("data-intro"),
                    step: parseInt(d.getAttribute("data-step"), 10),
                    tooltipClass: d.getAttribute("data-tooltipClass"),
                    position: d.getAttribute("data-position") || this._options.tooltipPosition
                })
            }
            var g = 0;
            for (var s = 0, p = r.length; s < p; s++) {
                var d = r[s];
                if (d.getAttribute("data-step") == null) {
                    for (;;) {
                        if (typeof t[g] == "undefined") break;
                        g++
                    }
                    t[g] = {
                        element: d,
                        intro: d.getAttribute("data-intro"),
                        step: g + 1,
                        tooltipClass: d.getAttribute("data-tooltipClass"),
                        position: d.getAttribute("data-position") || this._options.tooltipPosition
                    }
                }
            }
        }
        var y = [];
        for (var b = 0; b < t.length; b++) t[b] && y.push(t[b]);
        t = y, t.sort(function(e, t) {
            return e.step - t.step
        }), n._introItems = t;
        if (v.call(n, e)) {
            o.call(n);
            var w = e.querySelector(".introjs-skipbutton"),
                E = e.querySelector(".introjs-nextbutton");
            n._onKeyDown = function(t) {
                if (t.keyCode === 27 && n._options.exitOnEsc == 1) a.call(n, e), n._introExitCallback != undefined && n._introExitCallback.call(n);
                else if (t.keyCode === 37) u.call(n);
                else if (t.keyCode === 39 || t.keyCode === 13) o.call(n), t.preventDefault ? t.preventDefault() : t.returnValue = !1
            }, n._onResize = function(e) {
                l.call(n, document.querySelector(".introjs-helperLayer"))
            }, window.addEventListener ? (this._options.keyboardNavigation && window.addEventListener("keydown", n._onKeyDown, !0), window.addEventListener("resize", n._onResize, !0)) : document.attachEvent && (this._options.keyboardNavigation && document.attachEvent("onkeydown", n._onKeyDown), document.attachEvent("onresize", n._onResize))
        }
        return !1
    }

    function i(e) {
        if (e == null || typeof e != "object" || typeof e.nodeType != "undefined") return e;
        var t = {};
        for (var n in e) t[n] = i(e[n]);
        return t
    }

    function s(e) {
        this._currentStep = e - 2, typeof this._introItems != "undefined" && o.call(this)
    }

    function o() {
        this._direction = "forward", typeof this._currentStep == "undefined" ? this._currentStep = 0 : ++this._currentStep;
        if (this._introItems.length <= this._currentStep) {
            typeof this._introCompleteCallback == "function" && this._introCompleteCallback.call(this), a.call(this, this._targetElement);
            return
        }
        var e = this._introItems[this._currentStep];
        typeof this._introBeforeChangeCallback != "undefined" && this._introBeforeChangeCallback.call(this, e.element), c.call(this, e)
    }

    function u() {
        this._direction = "backward";
        if (this._currentStep === 0) return !1;
        var e = this._introItems[--this._currentStep];
        typeof this._introBeforeChangeCallback != "undefined" && this._introBeforeChangeCallback.call(this, e.element), c.call(this, e)
    }

    function a(e) {
        var t = e.querySelector(".introjs-overlay");
        if (t == null) return;
        t.style.opacity = 0, setTimeout(function() {
            t.parentNode && t.parentNode.removeChild(t)
        }, 500);
        var n = e.querySelector(".introjs-helperLayer");
        n && n.parentNode.removeChild(n);
        var r = document.querySelector(".introjsFloatingElement");
        r && r.parentNode.removeChild(r);
        var i = document.querySelector(".introjs-showElement");
        i && (i.className = i.className.replace(/introjs-[a-zA-Z]+/g, "").replace(/^\s+|\s+$/g, ""));
        var s = document.querySelectorAll(".introjs-fixParent");
        if (s && s.length > 0)
            for (var o = s.length - 1; o >= 0; o--) s[o].className = s[o].className.replace(/introjs-fixParent/g, "").replace(/^\s+|\s+$/g, "");
        window.removeEventListener ? window.removeEventListener("keydown", this._onKeyDown, !0) : document.detachEvent && document.detachEvent("onkeydown", this._onKeyDown), this._currentStep = undefined
    }

    function f(e, t, n, r) {
        t.style.top = null, t.style.right = null, t.style.bottom = null, t.style.left = null, t.style.marginLeft = null, t.style.marginTop = null, n.style.display = "inherit", typeof r != "undefined" && r != null && (r.style.top = null, r.style.left = null);
        if (!this._introItems[this._currentStep]) return;
        var i = "",
            s = this._introItems[this._currentStep];
        typeof s.tooltipClass == "string" ? i = s.tooltipClass : i = this._options.tooltipClass, t.className = ("introjs-tooltip " + i).replace(/^\s+|\s+$/g, "");
        var i = this._options.tooltipClass,
            o = this._introItems[this._currentStep].position;
        switch (o) {
            case "top":
                t.style.left = "15px", t.style.top = "-" + (m(t).height + 10) + "px", n.className = "introjs-arrow bottom";
                break;
            case "right":
                t.style.left = m(e).width + 20 + "px", n.className = "introjs-arrow left";
                break;
            case "left":
                this._options.showStepNumbers == 1 && (t.style.top = "15px"), t.style.right = m(e).width + 20 + "px", n.className = "introjs-arrow right";
                break;
            case "floating":
                n.style.display = "none";
                var u = m(t);
                t.style.left = "50%", t.style.top = "50%", t.style.marginLeft = "-" + u.width / 2 + "px", t.style.marginTop = "-" + u.height / 2 + "px", typeof r != "undefined" && r != null && (r.style.left = "-" + (u.width / 2 + 18) + "px", r.style.top = "-" + (u.height / 2 + 18) + "px");
                break;
            case "bottom":
            default:
                t.style.bottom = "-" + (m(t).height + 10) + "px", n.className = "introjs-arrow top"
        }
    }

    function l(e) {
        if (e) {
            if (!this._introItems[this._currentStep]) return;
            var t = this._introItems[this._currentStep],
                n = m(t.element),
                r = 10;
            t.position == "floating" && (r = 0), e.setAttribute("style", "width: " + (n.width + r) + "px; " + "height:" + (n.height + r) + "px; " + "top:" + (n.top - 5) + "px;" + "left: " + (n.left - 5) + "px;")
        }
    }

    function c(e) {
        typeof this._introChangeCallback != "undefined" && this._introChangeCallback.call(this, e.element);
        var t = this,
            n = document.querySelector(".introjs-helperLayer"),
            r = m(e.element);
        if (n != null) {
            var i = n.querySelector(".introjs-helperNumberLayer"),
                s = n.querySelector(".introjs-tooltiptext"),
                c = n.querySelector(".introjs-arrow"),
                v = n.querySelector(".introjs-tooltip"),
                g = n.querySelector(".introjs-skipbutton"),
                y = n.querySelector(".introjs-prevbutton"),
                b = n.querySelector(".introjs-nextbutton");
            v.style.opacity = 0;
            if (i != null) {
                var w = this._introItems[e.step - 2 >= 0 ? e.step - 2 : 0];
                if (w != null && this._direction == "forward" && w.position == "floating" || this._direction == "backward" && e.position == "floating") i.style.opacity = 0
            }
            l.call(t, n);
            var E = document.querySelectorAll(".introjs-fixParent");
            if (E && E.length > 0)
                for (var S = E.length - 1; S >= 0; S--) E[S].className = E[S].className.replace(/introjs-fixParent/g, "").replace(/^\s+|\s+$/g, "");
            var x = document.querySelector(".introjs-showElement");
            x.className = x.className.replace(/introjs-[a-zA-Z]+/g, "").replace(/^\s+|\s+$/g, ""), t._lastShowElementTimer && clearTimeout(t._lastShowElementTimer), t._lastShowElementTimer = setTimeout(function() {
                i != null && (i.innerHTML = e.step), s.innerHTML = e.intro, f.call(t, e.element, v, c, i), n.querySelector(".introjs-bullets li > a.active").className = "", n.querySelector('.introjs-bullets li > a[data-stepnumber="' + e.step + '"]').className = "active", v.style.opacity = 1, i.style.opacity = 1
            }, 350)
        } else {
            var T = document.createElement("div"),
                N = document.createElement("div"),
                C = document.createElement("div"),
                k = document.createElement("div"),
                L = document.createElement("div"),
                A = document.createElement("div");
            T.className = "introjs-helperLayer", l.call(t, T), this._targetElement.appendChild(T), N.className = "introjs-arrow", k.className = "introjs-tooltiptext", k.innerHTML = e.intro, L.className = "introjs-bullets", this._options.showBullets === !1 && (L.style.display = "none");
            var O = document.createElement("ul");
            for (var S = 0, M = this._introItems.length; S < M; S++) {
                var _ = document.createElement("li"),
                    D = document.createElement("a");
                D.onclick = function() {
                    t.goToStep(this.getAttribute("data-stepnumber"))
                }, S === 0 && (D.className = "active"), D.href = "javascript:void(0);", D.innerHTML = "&nbsp;", D.setAttribute("data-stepnumber", this._introItems[S].step), _.appendChild(D), O.appendChild(_)
            }
            L.appendChild(O), A.className = "introjs-tooltipbuttons", this._options.showButtons === !1 && (A.style.display = "none"), C.className = "introjs-tooltip", C.appendChild(k), C.appendChild(L);
            if (this._options.showStepNumbers == 1) {
                var P = document.createElement("span");
                P.className = "introjs-helperNumberLayer", P.innerHTML = e.step, T.appendChild(P)
            }
            C.appendChild(N), T.appendChild(C);
            var b = document.createElement("a");
            b.onclick = function() {
                t._introItems.length - 1 != t._currentStep && o.call(t)
            }, b.href = "javascript:void(0);", b.innerHTML = this._options.nextLabel;
            var y = document.createElement("a");
            y.onclick = function() {
                t._currentStep != 0 && u.call(t)
            }, y.href = "javascript:void(0);", y.innerHTML = this._options.prevLabel;
            var g = document.createElement("a");
            g.className = "introjs-button introjs-skipbutton", g.href = "javascript:void(0);", g.innerHTML = this._options.skipLabel, g.onclick = function() {
                t._introItems.length - 1 == t._currentStep && typeof t._introCompleteCallback == "function" && t._introCompleteCallback.call(t), t._introItems.length - 1 != t._currentStep && typeof t._introExitCallback == "function" && t._introExitCallback.call(t), a.call(t, t._targetElement)
            }, A.appendChild(g), this._introItems.length > 1 && (A.appendChild(y), A.appendChild(b)), C.appendChild(A), f.call(t, e.element, C, N, P)
        }
        this._currentStep == 0 && this._introItems.length > 1 ? (y.className = "introjs-button introjs-prevbutton introjs-disabled", b.className = "introjs-button introjs-nextbutton", g.innerHTML = this._options.skipLabel) : this._introItems.length - 1 == this._currentStep || this._introItems.length == 1 ? (g.innerHTML = this._options.doneLabel, y.className = "introjs-button introjs-prevbutton", b.className = "introjs-button introjs-nextbutton introjs-disabled") : (y.className = "introjs-button introjs-prevbutton", b.className = "introjs-button introjs-nextbutton", g.innerHTML = this._options.skipLabel), b.focus(), e.element.className += " introjs-showElement";
        var H = h(e.element, "position");
        H !== "absolute" && H !== "relative" && (e.element.className += " introjs-relativePosition");
        var B = e.element.parentNode;
        while (B != null) {
            if (B.tagName.toLowerCase() === "body") break;
            var j = h(B, "z-index"),
                F = parseFloat(h(B, "opacity"));
            if (/[0-9]+/.test(j) || F < 1) B.className += " introjs-fixParent";
            B = B.parentNode
        }
        if (!d(e.element) && this._options.scrollToElement === !0) {
            var I = e.element.getBoundingClientRect(),
                q = p().height,
                R = I.bottom - (I.bottom - I.top),
                U = I.bottom - q;
            R < 0 || e.element.clientHeight > q ? window.scrollBy(0, R - 30) : window.scrollBy(0, U + 100)
        }
        typeof this._introAfterChangeCallback != "undefined" && this._introAfterChangeCallback.call(this, e.element)
    }

    function h(e, t) {
        var n = "";
        return e.currentStyle ? n = e.currentStyle[t] : document.defaultView && document.defaultView.getComputedStyle && (n = document.defaultView.getComputedStyle(e, null).getPropertyValue(t)), n && n.toLowerCase ? n.toLowerCase() : n
    }

    function p() {
        if (window.innerWidth != undefined) return {
            width: window.innerWidth,
            height: window.innerHeight
        };
        var e = document.documentElement;
        return {
            width: e.clientWidth,
            height: e.clientHeight
        }
    }

    function d(e) {
        var t = e.getBoundingClientRect();
        return t.top >= 0 && t.left >= 0 && t.bottom + 80 <= window.innerHeight && t.right <= window.innerWidth
    }

    function v(e) {
        var t = document.createElement("div"),
            n = "",
            r = this;
        t.className = "introjs-overlay";
        if (e.tagName.toLowerCase() === "body") n += "top: 0;bottom: 0; left: 0;right: 0;position: fixed;", t.setAttribute("style", n);
        else {
            var i = m(e);
            i && (n += "width: " + i.width + "px; height:" + i.height + "px; top:" + i.top + "px;left: " + i.left + "px;", t.setAttribute("style", n))
        }
        return e.appendChild(t), t.onclick = function() {
            r._options.exitOnOverlayClick == 1 && (a.call(r, e), r._introExitCallback != undefined && r._introExitCallback.call(r))
        }, setTimeout(function() {
            n += "opacity: .8;", t.setAttribute("style", n)
        }, 10), !0
    }

    function m(e) {
        var t = {};
        t.width = e.offsetWidth, t.height = e.offsetHeight;
        var n = 0,
            r = 0;
        while (e && !isNaN(e.offsetLeft) && !isNaN(e.offsetTop)) n += e.offsetLeft, r += e.offsetTop, e = e.offsetParent;
        return t.top = r, t.left = n, t
    }

    function g(e, t) {
        var n = {};
        for (var r in e) n[r] = e[r];
        for (var r in t) n[r] = t[r];
        return n
    }
    var t = "0.8.0",
        y = function(e) {
            if (typeof e == "object") return new n(e);
            if (typeof e == "string") {
                var t = document.querySelector(e);
                if (t) return new n(t);
                throw new Error("There is no element with given selector.")
            }
            return new n(document.body)
        };
    return y.version = t, y.fn = n.prototype = {
        clone: function() {
            return new n(this)
        },
        setOption: function(e, t) {
            return this._options[e] = t, this
        },
        setOptions: function(e) {
            return this._options = g(this._options, e), this
        },
        start: function() {
            return r.call(this, this._targetElement), this
        },
        goToStep: function(e) {
            return s.call(this, e), this
        },
        nextStep: function() {
            return o.call(this), this
        },
        previousStep: function() {
            return u.call(this), this
        },
        exit: function() {
            a.call(this, this._targetElement)
        },
        refresh: function() {
            return l.call(this, document.querySelector(".introjs-helperLayer")), this
        },
        onbeforechange: function(e) {
            if (typeof e != "function") throw new Error("Provided callback for onbeforechange was not a function");
            return this._introBeforeChangeCallback = e, this
        },
        onchange: function(e) {
            if (typeof e != "function") throw new Error("Provided callback for onchange was not a function.");
            return this._introChangeCallback = e, this
        },
        onafterchange: function(e) {
            if (typeof e != "function") throw new Error("Provided callback for onafterchange was not a function");
            return this._introAfterChangeCallback = e, this
        },
        oncomplete: function(e) {
            if (typeof e != "function") throw new Error("Provided callback for oncomplete was not a function.");
            return this._introCompleteCallback = e, this
        },
        onexit: function(e) {
            if (typeof e != "function") throw new Error("Provided callback for onexit was not a function.");
            return this._introExitCallback = e, this
        }
    }, e.introJs = y, y
}), window.params = getSearchParameters(),
    function() {
        var e, t, n, r, i, s, o, u, a, f, l, c, h, p, d, v, m, g, y, b, w, E, S = {}.hasOwnProperty,
            x = function(e, t) {
                function r() {
                    this.constructor = e
                }
                for (var n in t) S.call(t, n) && (e[n] = t[n]);
                return r.prototype = t.prototype, e.prototype = new r, e.__super__ = t.prototype, e
            },
            T = [].indexOf || function(e) {
                for (var t = 0, n = this.length; t < n; t++)
                    if (t in this && this[t] === e) return t;
                return -1
            };
        y = function(e) {
            var t, n;
            return e == null && (e = null), n = document.URL, t = n.search(/[\?#]/), t !== -1 && (n = n.substring(0, t)), document.location = n + (e === null ? "" : "?" + e)
        }, m = function() {
            return navigator.userAgent.match(/(iPhone|iPod|iPad|Android)/i)
        }, o = function() {
            var e, t;
            return t = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
                function() {
                    var n, r;
                    r = [];
                    for (e = n = 1; n <= 8; e = ++n) r.push(t.charAt(Math.floor(Math.random() * t.length)));
                    return r
                }().join("")
        }, v = function(e) {
            return Math.floor((e + 1) * Math.random())
        }, w = function(e) {
            var t, n;
            return t = $("#" + e), n = $("#msg-popup"), n.is(":visible") ? t.siblings(":visible").fadeOut(function() {
                return t.fadeIn()
            }) : (t.show().siblings().hide(), n.fadeIn())
        }, b = function(e) {
            return $(".promo").hide(), e && typeof u != "undefined" && u !== null && u >= 1 && !g ? $(".promo-won-" + u).show() : $(".promo-" + (v(2) + 1)).show()
        }, E = [], a = function(e) {
            return e.length >= 4 ? "." : e + "."
        }, c = function(e) {
            return e.text(a(e.text()))
        }, f = function(e, t) {
            return e.attr(t, a(e.attr(t)))
        }, l = null, p = 0, d = "", g = !1, u = null, h = function(e, t, n, r) {
            n == null && (n = null), r == null && (r = null);
            if (typeof _gaq != "undefined" && _gaq !== null) return _gaq.push(["_trackEvent", e, t, n, r])
        }, window.onerror = function(e, t, n) {
            return h("js-error", e, "" + t + " (" + n + ")")
        }, t = function() {
            function e(e, t) {
                var n;
                this.opts = $.extend({
                    color: e.css("color"),
                    seconds: 10,
                    fps: 30,
                    min_score: 0,
                    negative_tick_rate: .5
                }, t), this.tickTime = 1e3 / this.opts.fps, this.interval = null, this.remain = 1, this.height = e.height(), this.width = e.width(), this.x = this.width / 2, this.y = this.height / 2, this.radius = Math.min(this.width, this.height) / 2, this.top = 1.5 * Math.PI, this.$count = $('<div class="timer-count"></div>').appendTo(e).css("font-size", 1.6 * this.radius).css("padding-top", Math.floor(.2 * this.radius)).css("width", this.width), this.isCanvasSupported() && (n = $("<canvas class='timer-graphic' height='" + this.height + "' width='" + this.width + "'></canvas>").appendTo(e), this.ctx = n[0].getContext("2d"), this.redraw())
            }
            return e.prototype.isCanvasSupported = function() {
                var e;
                return e = document.createElement("canvas"), !!e.getContext && !!e.getContext("2d")
            }, e.prototype.start = function() {
                var e = this;
                return clearInterval(this.interval), this.interval = setInterval(function() {
                    return e.tick()
                }, this.tickTime), this
            }, e.prototype.pause = function() {
                return clearInterval(this.interval), this
            }, e.prototype.reset = function() {
                return this.remain = 1, this.before = null, this
            }, e.prototype.redraw = function() {
                return this.isCanvasSupported() && (this.ctx.clearRect(0, 0, this.width, this.height), this.ctx.beginPath(), this.ctx.moveTo(this.x, this.y), this.remain < 0 ? this.ctx.arc(this.x, this.y, this.radius, this.top, this.top - 2 * Math.PI * this.remain) : this.ctx.arc(this.x, this.y, this.radius, this.top - 2 * Math.PI * this.remain, this.top), this.ctx.closePath(), this.ctx.fillStyle = this.remain < 0 ? "#a00" : this.opts.color, this.ctx.fill()), this.$count.text(parseInt(Math.ceil(this.time()))), this
            }, e.prototype.tick = function() {
                var e, t, n;
                return n = new Date, t = this.before ? n.getTime() - this.before.getTime() : 0, e = t > this.tickTime ? Math.floor(t / this.opts.fps) : 1, this.remain < 0 && (e = this.opts.negative_tick_rate * e), this.remain -= e / (this.phase_seconds() * this.opts.fps), this.remain <= -1 && (this.remain = -1, this.pause(), $(document).trigger("shiritori:timeout")), this.redraw(), this.before = n, this
            }, e.prototype.phase_seconds = function() {
                return this.remain < 0 ? Math.abs(this.opts.min_score) : this.opts.seconds
            }, e.prototype.time = function() {
                return this.remain * this.phase_seconds()
            }, e
        }(), n = function() {
            function e(e, n, r, i, s) {
                var o = this;
                this.players = e, this.startScore = n, this.startSeconds = r, this.minTurnScore = i, this.negativeTickRate = s, this.isFinished = !1, E = [], this.timer = new t(this.playerEl(this.players[0]).find(".timer"), {
                    seconds: this.startSeconds,
                    min_score: this.minTurnScore,
                    negative_tick_rate: this.negativeTickRate
                }), $(document).keydown(function(e) {
                    var t;
                    t = event.target || event.srcElement;
                    if (e.which === 8 && !$(t).is(":input")) return e.preventDefault()
                }), $(document).on("shiritori:moved", function(e, t, n, r) {
                    var i, s, u, a, f, l, c, v, m, g, y, S, x, T, N, C, k, L, A, O, M, _, D;
                    r == null && (r = null), m = n === "PASS", x = 4, S = m ? 0 : n.length - x, r === null ? (_ = Math.round(o.timer.time()), r = _ + S) : _ = r - S, t.acceptWord(n, r), N = $.grep(o.players, function(e) {
                        return e.id !== t.id
                    })[0], E.push(n), a = o.playerEl(t), s = o.playerEl(N), u = a.find(".score"), L = parseInt(u.text()), T = Math.max(0, u.text() - r), u.countTo({
                        from: L,
                        to: T,
                        speed: Math.max(r * 75, 250),
                        refreshInterval: 40,
                        onComplete: function(e) {
                            return $(this).text(e)
                        }
                    }), a.find(".score-change").text("" + r).show().fadeOut(2e3), y = n.length - 1, m || (o.startChar = n.charAt(y)), D = "<span class='non-scoring-chars" + (m ? " pass" : "") + "'>" + n.slice(0, x) + "</span>", n.length > x && (D += "<span class='scoring-chars'>" + n.slice(x, y + 1 || 9e9) + "</span>"), A = i18n.score_desc_length.replace(/%1%/g, n).replace(/%2%/g, S).replace(/%3%/g, x), O = i18n.score_desc_time.replace(/%1%/g, n).replace(/%2%/g, _), M = i18n.score_desc_total.replace(/%1%/g, n).replace(/%2%/g, r).replace(/%3%/g, S).replace(/%4%/g, _), i = $("<li class='past'><span class='word-wrap' title='" + n + "'>" + D + "</span><div class='word-score' title=\"" + M + "\"><div class='total-score'>" + r + "</div><div class='length-score' title=\"" + A + '">' + S + "</div><div class='time-score' title=\"" + O + "\"><span class='time-score-text'>" + _ + "</span><canvas></canvas></div></score></li>").hide(), o.timer.isCanvasSupported() && (f = a.find(".timer"), C = o.timer.opts.color, o.timer.opts.color = "#444", f.hide(), o.timer.remain = _ / o.startSeconds, o.timer.redraw(), c = i.find("canvas")[0], c.width = c.height = 2 * (Math.ceil(a.width() / 40) + 2), i.find(".length-score, .time-score").css("width", "" + c.width + "px"), i.find(".time-score-text").css("line-height", "" + c.width + "px"), c.getContext("2d").drawImage(f.find("canvas")[0], 0, 0, c.width, c.height), o.timer.opts.color = C, o.timer.redraw(), f.show()), t.count % 2 === 0 && i.addClass("odd"), a.find("ul").prepend(i), i.show().removeClass("past"), l = i.find(".word-wrap"), v = k = parseInt(l.css("font-size"));
                    while (l.width() > 180) v -= 1, l.css("font-size", v);
                    return t.endMove(), T === 0 ? (o.isFinished = !0, o.timer.pause(), a.addClass("won"), s.addClass("lost"), g = a.attr("id") === "left", $("#winner").text(i18n[g ? "you_won" : "you_lost"]), h("game", "end", "" + (g ? "won" : "lost") + " (" + d + ") (end game " + p + ")"), b(g), setTimeout(function() {
                        return w("end-game")
                    }, 500)) : (s.addClass("active").find(".timer").append(a.find(".timer").children()), o.timer.reset().start(), N.startMove(o.startChar)), a.removeClass("active")
                }), this.start()
            }
            return e.prototype.start = function() {
                var e, t, n, r, i;
                this.isFinished = !1, this.startChar = String.fromCharCode(97 + v(25)), i = this.players;
                for (n = 0, r = i.length; n < r; n++) t = i[n], t.score = this.startScore, this.playerEl(t).removeClass("lost won").find(".score").text(this.startScore), e = this.playerEl(t).siblings(".title-wrap"), e.find(".title").attr("title", t.name).text(t.name), e.css("opacity", "1");
                return this.players[0].startMove(this.startChar), this.playerEl(this.players[0]).addClass("active"), this.timer.start()
            }, e.prototype.resetDict = function() {
                return E = []
            }, e.prototype.reset = function() {
                var e, t, n, r;
                r = this.players;
                for (t = 0, n = r.length; t < n; t++) e = r[t], e.score = this.startScore;
                return this.playerEl(this.players[0]).find(".timer").append($(".timer").children()), this.timer.reset(), $(".history").empty(), this.resetDict(), this.start()
            }, e.prototype.finish = function() {
                var e, t, n, r;
                $(document).off("shiritori:moved"), r = this.players;
                for (t = 0, n = r.length; t < n; t++) e = r[t], e.reset(), this.playerEl(e).removeClass("active lost won"), this.playerEl(e).find("input").off("keydown blur").val("").attr("placeholder", "");
                return $(".history, .score, .timer").empty(), this.resetDict()
            }, e.prototype.playerEl = function(e) {
                return $("#" + e.id)
            }, e
        }(), i = function() {
            function e(e, t, n) {
                this.store = e, this.name = t, this.id = n, this.count = 0
            }
            return e.prototype.reset = function() {
                return this.count = 0, clearInterval(l)
            }, e.prototype.acceptWord = function(e, t) {
                return this.count += 1
            }, e.prototype.startMove = function(e) {
                var t = this;
                return this.$input.attr("placeholder", "..."), l = setInterval(function() {
                    return f(t.$input, "placeholder")
                }, 500)
            }, e.prototype.endMove = function() {
                return this.$input.val("").attr("placeholder", ""), clearInterval(l)
            }, e
        }(), s = function(e) {
            function t(e, n, r, i) {
                var s = this;
                this.$input = r, i == null && (i = null), t.__super__.constructor.call(this, e, i != null ? i : i18n.opponent, n), this.store !== null && this.store.on("child_added", function(e) {
                    var t;
                    return t = e.val(), $(document).trigger("shiritori:moved", [s, t.split(",")[0], t.split(",")[1]])
                })
            }
            return x(t, e), t.prototype.startMove = function(e) {
                return t.__super__.startMove.call(this, e)
            }, t
        }(i), r = function(e) {
            function t(e, n, r, i, s) {
                var o = this;
                this.$input = r, this.$error = i, s == null && (s = null), t.__super__.constructor.call(this, e, s != null ? s : i18n.you, n), this.$input.val("").attr("placeholder", ""), this.startChar = null, this.$input.on("paste", function(e) {
                    return o.error(i18n.no_copy_paste), e.preventDefault()
                }), this.$input.blur(function() {
                    return o._submitWord()
                }), this.$input.keydown(function(e) {
                    return e.which === 13 ? o._submitWord() : setTimeout(function() {
                        var e;
                        e = o.$input.val();
                        if (e.length > 0 && o.startChar != null && e.charAt(0) !== o.startChar) return o.error(i18n.err_wrong_start_char.replace("%1%", o.startChar))
                    }, 5)
                }), $(document).on("shiritori:timeout", function(e) {
                    if (o.$input.attr("disabled") !== "disabled") return o.error(""), $(document).trigger("shiritori:moved", [o, "PASS"])
                })
            }
            return x(t, e), t.prototype._submitWord = function() {
                var e;
                if (this.$input.attr("disabled") !== "disabled") return e = $.trim(this.$input.val().toLowerCase()), e.length === 0 ? (this.error(i18n.err_no_word), h("invalid-word", "no-word")) : this.startChar != null && e.charAt(0) !== this.startChar ? (this.error(i18n.err_wrong_start_char.replace("%1%", this.startChar)), h("invalid-word", "wrong-start-char", "start-char='" + this.startChar + "',word='" + e + "'")) : e.indexOf(" ") !== -1 ? (this.error(i18n.err_multiple_words), h("invalid-word", "multiple-words", "word='" + e + "'")) : e.length < 4 ? (this.error(i18n.err_too_short), h("invalid-word", "too-short", e)) : /^[a-zA-Z]+-[a-zA-Z]+$/.test(e) ? (this.error(i18n.err_no_hyphens), h("invalid-word", "hyphenated", e)) : T.call(dict[this.startChar], e) < 0 ? (this.error(i18n.err_not_in_dictionary.replace("%1%", e)), h("invalid-word", "not-in-dictionary", e)) : T.call(E, e) >= 0 ? (this.error(i18n.err_already_used.replace("%1%", e)), h("invalid-word", "already-used")) : (this.error(""), E.length <= 1 && h("game", "first-word", this.id), $(document).trigger("shiritori:moved", [this, e]))
            }, t.prototype.startMove = function(e) {
                return this.startChar = e, this.$input.removeAttr("disabled").focus(), $("html").hasClass("placeholder") ? this.$input.val("").attr("placeholder", e) : this.$input.val(e)
            }, t.prototype.endMove = function() {
                t.__super__.endMove.call(this), this.$input.attr("disabled", "disabled");
                if (!navigator.userAgent.match(/(iPod|iPhone|iPad)/i)) return this.$input.blur()
            }, t.prototype.acceptWord = function(e, n) {
                t.__super__.acceptWord.call(this, e, n);
                if (this.store != null) return this.store.push("" + e + "," + n)
            }, t.prototype.error = function(e) {
                return this.$error.html(e)
            }, t.prototype.reset = function() {
                return this.store != null && this.store.parent().remove(), this.$error.html(""), this.startChar = null, t.__super__.reset.apply(this, arguments)
            }, t
        }(i), e = function(e) {
            function t(e, n, r, i) {
                this.$input = n, this.aiSpeed = r, this.iq = i, this.isActive = !0, t.__super__.constructor.call(this, null, "A.I.", e)
            }
            return x(t, e), t.prototype.startMove = function(e) {
                var n, r, i, s, o, u, a, f = this;
                t.__super__.startMove.call(this, e), this.$input.removeAttr("disabled"), s = this.pickWord(e), i = this.aiSpeed + v(3 * this.aiSpeed), o = function(e, t, n) {
                    return setTimeout(function() {
                        if (n.isActive) return n.$input.val(n.$input.val() + e)
                    }, t)
                };
                for (r = u = 0, a = s.length; 0 <= a ? u <= a : u >= a; r = 0 <= a ? ++u : --u) i += .2 * this.aiSpeed + v(.15 * this.aiSpeed), n = s.charAt(r), o(n, i, this);
                return setTimeout(function() {
                    return $(document).trigger("shiritori:moved", [f, s])
                }, i + v(250))
            }, t.prototype.pickWord = function(e) {
                var t, n;
                return n = function() {
                    var n, r, i, s;
                    i = dict[e], s = [];
                    for (n = 0, r = i.length; n < r; n++) t = i[n], T.call(E, t) < 0 && s.push(t);
                    return s
                }(),
                    function() {
                        var e, t, r;
                        r = [];
                        for (e = 1, t = this.iq.from; 1 <= t ? e <= t : e >= t; 1 <= t ? e++ : e--) r.push(n[v(n.length - 1)]);
                        return r
                    }.call(this).sort(function(e, t) {
                        return e.length - t.length
                    })[this.iq.pick - 1]
            }, t.prototype.reset = function() {
                return this.isActive = !1, t.__super__.reset.apply(this, arguments)
            }, t
        }(i), $(function() {
            var t, i, a, f, v, m, b, E, S, x, T, N, C, k, L, A, O, M, _, D, P, H, B, j, F, I, q, R, U;
            i = 1, t = 0, f = 1, a = 0, S = null, R = o(), U = null, T = null, x = null, m = null, O = null, N = null, C = null, k = null, M = !1, _ = !1, v = null, j = function(e) {
                return g ? $("#msg-popup").fadeOut(e) : ($("#start-game .countdown").html("3"), w("start-game"), setInterval(function() {
                    return c($("#start-game .ellipsis"))
                }, 1e3), setTimeout(function() {
                    return $("#start-game .countdown").html("2"), setTimeout(function() {
                        return $("#start-game .countdown").html("1"), setTimeout(function() {
                            return $("#start-game .countdown").html("&nbsp;"), $("#msg-popup").fadeOut(e)
                        }, 1e3)
                    }, 1e3)
                }, 1e3))
            }, B = function(t, i, s, o, u, a) {
                return j(function(t, i, s, o, u, a) {
                    return function() {
                        S = new n([new r(null, "left", $("#left input"), $("#left .error"), get_display_user_name()), new e("right", $("#right input"), u, a)], t, i, s, o);
                        if (!g) {
                            p += 1;
                            if (!g) return h("game", "start", "AI game (" + d + ") (start game " + p + ")")
                        }
                    }
                }(t, i, s, o, u, a))
            }, q = function() {
                return v = new FirebaseSimpleLogin(O, function(e, t) {
                    return e ? alert("Could not connect to server") : t ? U = t.uid : U = null
                }), v.login("anonymous")
            }, F = function(e, t, i, o) {
                return M = !1, _ = !1, O != null && O.off(), e.on("value", function(e) {
                    if (e.val() === null && S && !S.isFinished) return $("#opponent-disconnected p").html(i18n.opponent_disconnected_body.replace(/%1%/, S.players[o ? 1 : 0].name)), w("opponent-disconnected")
                }), j(function(e, t, i, o) {
                    return function() {
                        var e;
                        return p += 1, $("#find-game .invite").hide(), clearInterval(l), o ? e = [new r(t.child("a"), "left", $("#left input"), $("#left .error"), get_display_user_name()), new s(t.child("b"), "right", $("#right input"), i)] : e = [new s(t.child("a"), "right", $("#right input"), i), new r(t.child("b"), "left", $("#left input"), $("#left .error"), get_display_user_name())], S = new n(e, 150, 12, -5, .5), h("game", "start", "Multiplayer game as " + (o ? "host" : "client") + " (start game " + p + ")")
                    }
                }(e, t, i, o))
            }, E = function() {
                return h("game", "connection", "Host multiplayer game"), C = O.push({
                    h: R,
                    n: get_display_user_name(),
                    x: get_display_avatar_url(),
                    s: f,
                    ".priority": i
                }), C.onDisconnect().remove(), k = N.child(C.name()), k.onDisconnect().remove(), k.set({
                    h: U
                }), k.child("y").on("value", function(e) {
                    return H(e.val())
                }), k.child("m").on("value", function(e) {
                    if (e.val() !== null) return C.setPriority(t), C.update({
                        s: a
                    }), F(C, k, e.val(), !0)
                }), setTimeout(D, 8e3)
            }, L = function() {
                return O.startAt(i).limit(100).on("child_added", function(e) {
                    var t, n;
                    if (e.val().h !== R && e.val().s === f) {
                        n = $("#find-game .hosts");
                        if (!n.find("[data-game-id=" + e.name() + "]").length) return t = $("<a class='open-game truncate' data-game-id='" + e.name() + "'>" + e.val().n + "</a>").hide(), e.val().x && t.css("background-image", "url(" + e.val().x + ")"), n.append(t), t.fadeIn(), e.ref().on("value", function(e) {
                            var t, r;
                            if (e.val() === null || ((t = e.val()) != null ? t.s : void 0) !== f) return H((r = e.val()) != null ? r.x : void 0), n.find("[data-game-id=" + e.name() + "]").fadeOut(function() {
                                return $(this).remove()
                            })
                        })
                    }
                })
            }, $(document).on("click", "#find-game .open-game", function() {
                var e, t, n, r = this;
                return e = $(this).data("game-id"), t = O.child(e), n = N.child(e), n.update({
                    c: U,
                    m: get_display_user_name(),
                    y: get_display_avatar_url()
                }, function(e) {
                    return e === null ? (P(), n.onDisconnect().remove(), t.onDisconnect().remove(), F(t, n, $(r).text(), !1)) : $(r).remove()
                })
            }), D = function() {
                if (M) return $(".game-search-msg").fadeIn()
            }, P = function() {
                C != null && C.remove();
                if (k != null) return k.remove()
            }, b = function() {
                return h("game", "connection", "Cancel multiplayer search"), P(), y()
            }, I = function() {
                return w("find-game"), m.child("vmin").on("value", function(e) {
                    return "1.0.0" < e.val() ? alert("Your Shiritori game version is out of date. Please refresh the page and try again.") : (h("game", "connection", "Search for multiplayer host"), M = !0, _ = !0, l = setInterval(function() {
                        return $("#find-game .ellipsis").each(function() {
                            return c($(this))
                        })
                    }, 500), L(), setTimeout(function() {
                        if (M) return _ = !1, E(), $(".game-connect-msg").fadeOut(1e3, function() {
                            return $(".game-wait-msg").fadeIn(1e3)
                        })
                    }, 4e3))
                })
            }, $("button.multiplayer-game").on("click", function() {
                return m = new Firebase("https://shiritori.firebaseio.com"), O = m.child("l"), N = m.child("g"), v === null && q(), I()
            }), $("button.multiplayer-cancel").on("click", function() {
                return w("help-body-simple"), b()
            }), $("button.ai-game").on("click", function() {
                return w("ai-opts")
            }), $("button.ai-game-cancel").on("click", function() {
                return y()
            }), $("button.ai-level-0").on("click", function() {
                return d = "Easy", u = 0, B(100, 15, -2, .2, 1500, {
                    pick: 1,
                    from: 10
                })
            }), $("button.ai-level-1").on("click", function() {
                return d = "Medium", u = 1, B(120, 12, -5, .5, 900, {
                    pick: 2,
                    from: 6
                })
            }), $("button.ai-level-2").on("click", function() {
                return d = "Hard", u = 2, B(150, 10, -8, 1, 750, {
                    pick: 3,
                    from: 4
                })
            }), $("button.ai-level-3").on("click", function() {
                return d = "Extreme", u = 3, B(150, 8, -8, 1, 600, {
                    pick: 4,
                    from: 4
                })
            }), $("button.replay").on("click", function() {
                return S.players[0] instanceof s || S.players[1] instanceof s ? (P(), y("replay=multiplayer")) : (h("game", "start", "Replay (start game " + p + ")"), y("replay=ai-" + u))
            }), $("button.menu").on("click", function() {
                return y()
            }), $("button.tutorial").on("click", function() {
                return $("#msg-popup").fadeOut(function() {
                    var e, t, n;
                    return g = !0, h("tutorial", "start"), $("button.ai-level-1").click(), n = {
                        doneLabel: i18n.tutorial_done,
                        nextLabel: i18n.tutorial_next,
                        prevLabel: i18n.tutorial_back,
                        skipLabel: i18n.tutorial_skip,
                        exitOnOverlayClick: !1,
                        showBullets: !1
                    }, t = introJs().setOptions(n).start(), e = function() {
                        return y()
                    }, t.oncomplete(function() {
                        return h("tutorial", "complete"), e()
                    }), t.onexit(function() {
                        return h("tutorial", "exit"), e()
                    }), t.onchange(function(e) {
                        return setTimeout(function() {
                            var n, r, i;
                            n = $(e), r = n.data("step");
                            if (r === 2 && $(".player1 .history").is(":empty") || r === 7 && $(".player1 .history li").length < 2) {
                                t.goToStep(r - 1), $(".player1 input").focus();
                                return
                            }
                            if (r === 6 && $(".player2 .history").is(":empty")) {
                                t.goToStep(r - 1);
                                return
                            }
                            h("tutorial", "step " + r), r !== 1 && r !== 2 && r !== 6 && r !== 7 && $("#left .error").text(""), t._introItems[r - 1].intro = t._introItems[r - 1].intro.replace(/%1%/, S.startChar), r === 6 && n.find("input").focus();
                            if (r === 8) return i = $(".sharing").outerHeight() + 10, $(".introjs-helperLayer").css("height", 0).css("top", i).css("border", "none")
                        }, 100)
                    })
                })
            }), $(".close-msg").click(function() {
                return $(this).parents(".msg").fadeOut()
            }), window.get_display_user_name = function() {
                return T
            }, window.set_display_user_name = function(e) {
                return T = e, C != null && C.update({
                    n: e
                }), $(".greeting-name-show").text(e).attr("title", e), $(".avatar-local").attr("title", e)
            }, window.get_display_avatar_url = function() {
                return x
            }, window.set_display_avatar_url = function(e) {
                e == null && (e = null), x = e, $(".avatar-local").css("background-image", "url(" + (e != null ? e : "../images/user-6bb69a40.png") + ")");
                if (C != null) return C.update({
                    x: e
                })
            }, H = function(e) {
                return $("#right").siblings(".title-wrap").find(".avatar").css("background-image", "url(" + (e ? e : "../images/user-6bb69a40.png") + ")")
            }, window.get_anon_user_name = function() {
                var e;
                return e = $.trim($.cookie("name")), e == null || e === "" ? "Guest" : e
            }, window.set_anon_user_name = function(e) {
                if (e != null) {
                    e = $.trim(e).substring(0, 100);
                    if (e !== "") return $.cookie("name", e), set_display_user_name(e)
                }
            }, set_anon_user_name(get_anon_user_name()), $(".greeting-name-change").click(function() {
                return set_anon_user_name(prompt("Enter your screen name, which will be visible to others in multiplayer mode.", get_anon_user_name()))
            }), $(".game input").val("").attr("disabled", "disabled"), $(".player").click(function() {
                return $(this).find("input").focus()
            }), $(".lang-switch a").click(function(e) {
                if (M || S != null) {
                    if (!confirm(i18n.confirm_exit_game)) return !1;
                    if (M) return b()
                }
            }), $(".sharing a").on("click", function(e) {
                return h("social", "click", $(this).attr("href"))
            });
            if (document.URL.match(/\?replay=multiplayer$/)) return $("button.multiplayer-game").click();
            if (document.URL.match(/\?replay=ai-\d$/)) return A = /\?replay=ai-(\d)$/.exec(document.URL), $("button.ai-level-" + A[1]).click()
        })
    }.call(this);