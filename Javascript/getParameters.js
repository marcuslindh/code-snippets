function getParameters(url) {
    if (!url) {
        url = window.location.search;
    }

    var perms = {};

    var s = url.split(/[?&]+([^=&]+)=([^&]*)/gi);

    for (var i = 1; i < s.length; i++) {

        if (decodeURIComponent(s[i + 1]).indexOf(",") > -1) {
            perms[s[i]] = decodeURIComponent(s[i + 1]).split(",");
        } else {
            perms[s[i]] = decodeURIComponent(s[i + 1]);
        }


        i++;
        i++;
    }

    return perms;
}