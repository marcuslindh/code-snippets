function inject(obj, rootname, beforeFn, n) {
    //console.log(obj);
    for (let propName of Object.getOwnPropertyNames(obj)) {
        let prop = obj[propName];
        if (Object.prototype.toString.call(prop) === '[object Function]') {
            obj[propName] = (function (fnName) {
                return function () {
                    beforeFn.call(this, fnName, rootname, arguments);
                    console.time(rootname + "." + propName);
                    var func = prop.apply(this, arguments);
                    console.timeEnd(rootname + "." + propName);
                    return func;
                }
            })(propName);
        } else if (Object.prototype.toString.call(prop) === "[object Object]") {
            if (n) {
                if (n < 5) {
                    inject(prop, rootname + "." + propName, logFnCall, n + 1);
                } else {
                    inject(prop, rootname + "." + propName, logFnCall, 1);
                }
            }
        }
    }
}

function logFnCall(name, rootname, args) {
    let s = name + '(';
    for (let i = 0; i < args.length; i++) {
        if (i > 0) {
            s += ', ';
        }
        if (String(args[i]) === "[object Object]") {
            try {
                s += JSON.stringify(args[i]);
            } catch (e) {
                s += String(args[i]);
            }
        } else {
            if (typeof args[i] === "function") {
                s += "function...";
                //console.log(args[i]);
            } else if (typeof args[i] === "string") {
                s += '"' + String(args[i]) + '"';
            } else {
                if (Object.prototype.toString.call(args[i]) === "[object Array]") {
                    s += '[';
                    for (var x = 0; x < args[i].length; x++) {
                        if (x > 0) {
                            s += ',';
                        }
                        if (typeof args[i][x] === "string") {
                            s += '"' + String(args[i][x]) + '"';
                        } else {
                            s += String(args[i][x]);
                        }

                    }
                    s += ']';
                    //s += '["' + String(args[i]) + '"]';
                } else {
                    s += String(args[i]);
                }
            }
        }
    }
    s += ')';
    console.log(rootname + "." + s);
}