/*!
 * Tooltip v1.0.16
 * https://sa-si-dev.github.io/tooltip
 * Licensed under MIT (https://github.com/sa-si-dev/tooltip/blob/master/LICENSE)
 */ !(function () {
  "use strict";
  function t(t) {
    return (
      (function (t) {
        if (Array.isArray(t)) return e(t);
      })(t) ||
      (function (t) {
        if (
          ("undefined" != typeof Symbol && null != t[Symbol.iterator]) ||
          null != t["@@iterator"]
        )
          return Array.from(t);
      })(t) ||
      (function (t, o) {
        if (t) {
          if ("string" == typeof t) return e(t, o);
          var i = Object.prototype.toString.call(t).slice(8, -1);
          return (
            "Object" === i && t.constructor && (i = t.constructor.name),
            "Map" === i || "Set" === i
              ? Array.from(t)
              : "Arguments" === i ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(i)
              ? e(t, o)
              : void 0
          );
        }
      })(t) ||
      (function () {
        throw new TypeError(
          "Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
        );
      })()
    );
  }
  function e(t, e) {
    (null == e || e > t.length) && (e = t.length);
    for (var o = 0, i = new Array(e); o < e; o++) i[o] = t[o];
    return i;
  }
  function o(t, e) {
    for (var o = 0; o < e.length; o++) {
      var i = e[o];
      (i.enumerable = i.enumerable || !1),
        (i.configurable = !0),
        "value" in i && (i.writable = !0),
        Object.defineProperty(t, i.key, i);
    }
  }
  var i = (function () {
    function e() {
      !(function (t, e) {
        if (!(t instanceof e))
          throw new TypeError("Cannot call a class as a function");
      })(this, e);
    }
    var i, n;
    return (
      (i = e),
      (n = [
        {
          key: "addClass",
          value: function (o, i) {
            o &&
              ((i = i.split(" ")),
              e.getElements(o).forEach(function (e) {
                var o;
                (o = e.classList).add.apply(o, t(i));
              }));
          },
        },
        {
          key: "removeClass",
          value: function (o, i) {
            o &&
              ((i = i.split(" ")),
              e.getElements(o).forEach(function (e) {
                var o;
                (o = e.classList).remove.apply(o, t(i));
              }));
          },
        },
        {
          key: "getElements",
          value: function (t) {
            if (t) return void 0 === t.forEach && (t = [t]), t;
          },
        },
        {
          key: "getMoreVisibleSides",
          value: function (t) {
            if (!t) return {};
            var e = t.getBoundingClientRect(),
              o = window.innerWidth,
              i = window.innerHeight,
              n = e.left,
              r = e.top;
            return {
              horizontal: n > o - n - e.width ? "left" : "right",
              vertical: r > i - r - e.height ? "top" : "bottom",
            };
          },
        },
        {
          key: "getAbsoluteCoords",
          value: function (t) {
            if (t) {
              var e = t.getBoundingClientRect(),
                o = window.pageXOffset,
                i = window.pageYOffset;
              return {
                width: e.width,
                height: e.height,
                top: e.top + i,
                right: e.right + o,
                bottom: e.bottom + i,
                left: e.left + o,
              };
            }
          },
        },
        {
          key: "getCoords",
          value: function (t) {
            return t ? t.getBoundingClientRect() : {};
          },
        },
        {
          key: "getData",
          value: function (t, e, o) {
            if (t) {
              var i = t ? t.dataset[e] : "";
              return (
                "number" === o
                  ? (i = parseFloat(i) || 0)
                  : "true" === i
                  ? (i = !0)
                  : "false" === i && (i = !1),
                i
              );
            }
          },
        },
        {
          key: "setData",
          value: function (t, e, o) {
            t && (t.dataset[e] = o);
          },
        },
        {
          key: "setStyle",
          value: function (t, e, o) {
            t && (t.style[e] = o);
          },
        },
        {
          key: "show",
          value: function (t) {
            var o =
              arguments.length > 1 && void 0 !== arguments[1]
                ? arguments[1]
                : "block";
            e.setStyle(t, "display", o);
          },
        },
        {
          key: "hide",
          value: function (t) {
            e.setStyle(t, "display", "none");
          },
        },
        {
          key: "getHideableParent",
          value: function (t) {
            for (var e, o = t.parentElement; o; ) {
              var i = getComputedStyle(o).overflow;
              if (-1 !== i.indexOf("scroll") || -1 !== i.indexOf("auto")) {
                e = o;
                break;
              }
              o = o.parentElement;
            }
            return e;
          },
        },
      ]) && o(i, n),
      e
    );
  })();
  function n(t, e) {
    for (var o = 0; o < e.length; o++) {
      var i = e[o];
      (i.enumerable = i.enumerable || !1),
        (i.configurable = !0),
        "value" in i && (i.writable = !0),
        Object.defineProperty(t, i.key, i);
    }
  }
  var r = ["top", "bottom", "left", "right"].map(function (t) {
      return "position-".concat(t);
    }),
    a = {
      top: "rotate(180deg)",
      left: "rotate(90deg)",
      right: "rotate(-90deg)",
    },
    l = (function () {
      function t(e) {
        !(function (t, e) {
          if (!(t instanceof e))
            throw new TypeError("Cannot call a class as a function");
        })(this, t);
        try {
          this.setProps(e), this.init();
        } catch (t) {
          console.warn("Couldn't initiate popper"), console.error(t);
        }
      }
      var e, o;
      return (
        (e = t),
        (o = [
          {
            key: "init",
            value: function () {
              var t = this.$popperEle;
              t &&
                this.$triggerEle &&
                (i.setStyle(t, "zIndex", this.zIndex), this.setPosition());
            },
          },
          {
            key: "setProps",
            value: function (t) {
              var e = (t = this.setDefaultProps(t)).position
                ? t.position.toLowerCase()
                : "auto";
              if (
                ((this.$popperEle = t.$popperEle),
                (this.$triggerEle = t.$triggerEle),
                (this.$arrowEle = t.$arrowEle),
                (this.margin = parseFloat(t.margin)),
                (this.offset = parseFloat(t.offset)),
                (this.enterDelay = parseFloat(t.enterDelay)),
                (this.exitDelay = parseFloat(t.exitDelay)),
                (this.showDuration = parseFloat(t.showDuration)),
                (this.hideDuration = parseFloat(t.hideDuration)),
                (this.transitionDistance = parseFloat(t.transitionDistance)),
                (this.zIndex = parseFloat(t.zIndex)),
                (this.afterShowCallback = t.afterShow),
                (this.afterHideCallback = t.afterHide),
                (this.hasArrow = !!this.$arrowEle),
                -1 !== e.indexOf(" "))
              ) {
                var o = e.split(" ");
                (this.position = o[0]), (this.secondaryPosition = o[1]);
              } else this.position = e;
            },
          },
          {
            key: "setDefaultProps",
            value: function (t) {
              return Object.assign(
                {
                  position: "auto",
                  margin: 8,
                  offset: 5,
                  enterDelay: 0,
                  exitDelay: 0,
                  showDuration: 300,
                  hideDuration: 200,
                  transitionDistance: 10,
                  zIndex: 1,
                },
                t
              );
            },
          },
          {
            key: "setPosition",
            value: function () {
              i.show(this.$popperEle, "inline-flex");
              var t,
                e,
                o,
                n = window.innerWidth,
                l = window.innerHeight,
                s = i.getAbsoluteCoords(this.$popperEle),
                p = i.getAbsoluteCoords(this.$triggerEle),
                u = s.width,
                c = s.height,
                f = s.top,
                h = s.right,
                d = s.bottom,
                v = s.left,
                y = p.width,
                m = p.height,
                g = p.top,
                w = p.right,
                E = p.bottom,
                D = p.left,
                b = g - f,
                x = D - v,
                $ = x,
                k = b,
                S = this.position,
                C = this.secondaryPosition,
                T = y / 2 - u / 2,
                P = m / 2 - c / 2,
                O = this.margin,
                A = this.transitionDistance,
                I = window.scrollY - f,
                z = l + I,
                H = window.scrollX - v,
                L = n + H,
                F = this.offset;
              F && ((I += F), (z -= F), (H += F), (L -= F)),
                "auto" === S &&
                  (S = i.getMoreVisibleSides(this.$triggerEle).vertical);
              var j = {
                  top: { top: k - c - O, left: $ + T },
                  bottom: { top: k + m + O, left: $ + T },
                  right: { top: k + P, left: $ + y + O },
                  left: { top: k + P, left: $ - u - O },
                },
                W = j[S];
              if (
                ((k = W.top),
                ($ = W.left),
                C &&
                  ("top" === C
                    ? (k = b)
                    : "bottom" === C
                    ? (k = b + m - c)
                    : "left" === C
                    ? ($ = x)
                    : "right" === C && ($ = x + y - u)),
                $ < H
                  ? "left" === S
                    ? (o = "right")
                    : ($ = H + v > w ? w - v : H)
                  : $ + u > L &&
                    ("right" === S
                      ? (o = "left")
                      : ($ = L + v < D ? D - h : L - u)),
                k < I
                  ? "top" === S
                    ? (o = "bottom")
                    : (k = I + f > E ? E - f : I)
                  : k + c > z &&
                    ("bottom" === S
                      ? (o = "top")
                      : (k = z + f < g ? g - d : z - c)),
                o)
              ) {
                var M = j[o];
                "top" === (S = o) || "bottom" === S
                  ? (k = M.top)
                  : ("left" !== S && "right" !== S) || ($ = M.left);
              }
              "top" === S
                ? ((t = k + A), (e = $))
                : "right" === S
                ? ((t = k), (e = $ - A))
                : "left" === S
                ? ((t = k), (e = $ + A))
                : ((t = k - A), (e = $));
              var B = "translate3d(".concat(e, "px, ").concat(t, "px, 0)");
              if (
                (i.setStyle(this.$popperEle, "transform", B),
                i.setData(this.$popperEle, "fromLeft", e),
                i.setData(this.$popperEle, "fromTop", t),
                i.setData(this.$popperEle, "top", k),
                i.setData(this.$popperEle, "left", $),
                i.removeClass(this.$popperEle, r.join(" ")),
                i.addClass(this.$popperEle, "position-".concat(S)),
                this.hasArrow)
              ) {
                var q = 0,
                  R = 0,
                  V = $ + v,
                  X = k + f,
                  Y = this.$arrowEle.offsetWidth / 2,
                  N = a[S] || "";
                "top" === S || "bottom" === S
                  ? (q = y / 2 + D - V) < Y
                    ? (q = Y)
                    : q > u - Y && (q = u - Y)
                  : ("left" !== S && "right" !== S) ||
                    ((R = m / 2 + g - X) < Y
                      ? (R = Y)
                      : R > c - Y && (R = c - Y)),
                  i.setStyle(
                    this.$arrowEle,
                    "transform",
                    "translate3d("
                      .concat(q, "px, ")
                      .concat(R, "px, 0) ")
                      .concat(N)
                  );
              }
              i.hide(this.$popperEle);
            },
          },
          {
            key: "resetPosition",
            value: function () {
              i.setStyle(this.$popperEle, "transform", "none"),
                this.setPosition();
            },
          },
          {
            key: "show",
            value: function () {
              var t = this,
                e =
                  arguments.length > 0 && void 0 !== arguments[0]
                    ? arguments[0]
                    : {},
                o = e.resetPosition,
                n = e.data;
              clearTimeout(this.exitDelayTimeout),
                clearTimeout(this.hideDurationTimeout),
                o && this.resetPosition(),
                (this.enterDelayTimeout = setTimeout(function () {
                  var e = i.getData(t.$popperEle, "left"),
                    o = i.getData(t.$popperEle, "top"),
                    r = "translate3d(".concat(e, "px, ").concat(o, "px, 0)"),
                    a = t.showDuration;
                  i.show(t.$popperEle, "inline-flex"),
                    i.getCoords(t.$popperEle),
                    i.setStyle(t.$popperEle, "transitionDuration", a + "ms"),
                    i.setStyle(t.$popperEle, "transform", r),
                    i.setStyle(t.$popperEle, "opacity", 1),
                    (t.showDurationTimeout = setTimeout(function () {
                      "function" == typeof t.afterShowCallback &&
                        t.afterShowCallback(n);
                    }, a));
                }, this.enterDelay));
            },
          },
          {
            key: "hide",
            value: function () {
              var t = this,
                e =
                  arguments.length > 0 && void 0 !== arguments[0]
                    ? arguments[0]
                    : {},
                o = e.data;
              clearTimeout(this.enterDelayTimeout),
                clearTimeout(this.showDurationTimeout),
                (this.exitDelayTimeout = setTimeout(function () {
                  if (t.$popperEle) {
                    var e = i.getData(t.$popperEle, "fromLeft"),
                      n = i.getData(t.$popperEle, "fromTop"),
                      r = "translate3d(".concat(e, "px, ").concat(n, "px, 0)"),
                      a = t.hideDuration;
                    i.setStyle(t.$popperEle, "transitionDuration", a + "ms"),
                      i.setStyle(t.$popperEle, "transform", r),
                      i.setStyle(t.$popperEle, "opacity", 0),
                      (t.hideDurationTimeout = setTimeout(function () {
                        i.hide(t.$popperEle),
                          "function" == typeof t.afterHideCallback &&
                            t.afterHideCallback(o);
                      }, a));
                  }
                }, this.exitDelay));
            },
          },
          {
            key: "updatePosition",
            value: function () {
              i.setStyle(this.$popperEle, "transitionDuration", "0ms"),
                this.resetPosition();
              var t = i.getData(this.$popperEle, "left"),
                e = i.getData(this.$popperEle, "top");
              i.show(this.$popperEle, "inline-flex"),
                i.setStyle(
                  this.$popperEle,
                  "transform",
                  "translate3d(".concat(t, "px, ").concat(e, "px, 0)")
                );
            },
          },
        ]) && n(e.prototype, o),
        t
      );
    })();
  window.PopperComponent = l;
})(),
  (function () {
    "use strict";
    function t(t, e) {
      for (var o = 0; o < e.length; o++) {
        var i = e[o];
        (i.enumerable = i.enumerable || !1),
          (i.configurable = !0),
          "value" in i && (i.writable = !0),
          Object.defineProperty(t, i.key, i);
      }
    }
    var e = (function () {
      function e() {
        !(function (t, e) {
          if (!(t instanceof e))
            throw new TypeError("Cannot call a class as a function");
        })(this, e);
      }
      var o, i, n;
      return (
        (o = e),
        (n = [
          {
            key: "convertToBoolean",
            value: function (t) {
              var e =
                arguments.length > 1 && void 0 !== arguments[1] && arguments[1];
              return (t =
                !0 === t || "true" === t || (!1 !== t && "false" !== t && e));
            },
          },
          {
            key: "convertToFloat",
            value: function (t) {
              var e =
                arguments.length > 1 && void 0 !== arguments[1]
                  ? arguments[1]
                  : 0;
              return void 0 !== t ? parseFloat(t) : e;
            },
          },
        ]),
        (i = null) && t(o.prototype, i),
        n && t(o, n),
        e
      );
    })();
    function o(t, e) {
      for (var o = 0; o < e.length; o++) {
        var i = e[o];
        (i.enumerable = i.enumerable || !1),
          (i.configurable = !0),
          "value" in i && (i.writable = !0),
          Object.defineProperty(t, i.key, i);
      }
    }
    var i = (function () {
      function t() {
        !(function (t, e) {
          if (!(t instanceof e))
            throw new TypeError("Cannot call a class as a function");
        })(this, t);
      }
      var e, i, n;
      return (
        (e = t),
        (n = [
          {
            key: "hasEllipsis",
            value: function (t) {
              return !!t && t.scrollWidth > t.offsetWidth;
            },
          },
          {
            key: "setStyle",
            value: function (t, e, o) {
              t && (t.style[e] = o);
            },
          },
          {
            key: "getScrollableParents",
            value: function (t) {
              if (!t) return [];
              for (var e = [window], o = t.parentElement; o; ) {
                var i = getComputedStyle(o).overflow;
                (-1 === i.indexOf("scroll") && -1 === i.indexOf("auto")) ||
                  e.push(o),
                  (o = o.parentElement);
              }
              return e;
            },
          },
        ]),
        (i = null) && o(e.prototype, i),
        n && o(e, n),
        t
      );
    })();
    !(function () {
      if (!window.tooltipComponentInitiated) {
        var t, o, n, r, a;
        window.tooltipComponentInitiated = !0;
        var l,
          s = {},
          p = !1;
        u(), window.addEventListener("load", u);
      }
      function u() {
        p ||
          ((t = document.querySelector("body")) &&
            (document.addEventListener("mouseover", c),
            document.addEventListener("mouseout", f),
            document.addEventListener("touchmove", h),
            document.addEventListener("click", d),
            (p = !0)));
      }
      function c(p) {
        if (!n) {
          var u = p.target.closest("[data-tooltip]");
          u &&
            ((n = u),
            (function () {
              if (n) {
                var p,
                  u = e.convertToBoolean,
                  c = e.convertToFloat,
                  f = n.dataset;
                if (
                  !(s = {
                    tooltip: f.tooltip,
                    position: f.tooltipPosition || "auto",
                    zIndex: c(f.tooltipZIndex, 1),
                    enterDelay: c(f.tooltipEnterDelay, 0),
                    exitDelay: c(f.tooltipExitDelay, 0),
                    fontSize: f.tooltipFontSize || "14px",
                    margin: c(f.tooltipMargin, 8),
                    offset: c(f.tooltipOffset, 5),
                    showDuration: c(f.tooltipShowDuration, 300),
                    hideDuration: c(f.tooltipHideDuration, 200),
                    transitionDistance: c(f.tooltipTransitionDistance, 10),
                    ellipsisOnly: u(f.tooltipEllipsisOnly),
                    allowHtml: u(f.tooltipAllowHtml),
                    hideOnClick: u(f.tooltipHideOnClick, !0),
                    hideArrowIcon: u(f.tooltipHideArrowIcon),
                    alignment: f.tooltipAlignment || "left",
                    maxWidth: f.tooltipMaxWidth || "300px",
                    additionalClasses: f.tooltipAdditionalClasses,
                  }).tooltip ||
                  (s.ellipsisOnly && !i.hasEllipsis(n))
                )
                  v();
                else
                  o && o.remove(),
                    (function () {
                      var e = "tooltip-comp";
                      s.hideArrowIcon && (e += " hide-arrow-icon"),
                        s.additionalClasses && (e += " " + s.additionalClasses);
                      var n = '<div class="'.concat(
                        e,
                        '">\n        <i class="tooltip-comp-arrow"></i>\n        <div class="tooltip-comp-content"></div>\n      </div>'
                      );
                      t.insertAdjacentHTML("beforeend", n);
                      var a = i.setStyle;
                      (o = document.querySelector(".tooltip-comp")),
                        (r = o.querySelector(".tooltip-comp-arrow"));
                      var l = o.querySelector(".tooltip-comp-content");
                      s.allowHtml
                        ? (l.innerHTML = s.tooltip)
                        : (l.innerText = s.tooltip),
                        a(o, "zIndex", s.zIndex),
                        a(o, "fontSize", s.fontSize),
                        a(o, "textAlign", s.alignment),
                        a(o, "maxWidth", s.maxWidth);
                    })(),
                    (p = {
                      $popperEle: o,
                      $triggerEle: n,
                      $arrowEle: r,
                      position: s.position,
                      margin: s.margin,
                      offset: s.offset,
                      enterDelay: s.enterDelay,
                      exitDelay: s.exitDelay,
                      showDuration: s.showDuration,
                      hideDuration: s.hideDuration,
                      transitionDistance: s.transitionDistance,
                      zIndex: s.zIndex,
                    }),
                    (l = new PopperComponent(p)).show(),
                    (a = i.getScrollableParents(n)).forEach(function (t) {
                      t.addEventListener("scroll", y);
                    });
              }
            })());
        }
      }
      function f(t) {
        if (n) {
          for (var e = t.relatedTarget; e; ) {
            if (e === n) return;
            e = e.parentNode;
          }
          v();
        }
      }
      function h() {
        v();
      }
      function d() {
        s.hideOnClick && v();
      }
      function v() {
        (o || n) &&
          (l && l.hide(),
          a &&
            (a.forEach(function (t) {
              t.removeEventListener("scroll", y);
            }),
            (a = null)),
          (n = null));
      }
      function y() {
        v();
      }
    })();
  })();
