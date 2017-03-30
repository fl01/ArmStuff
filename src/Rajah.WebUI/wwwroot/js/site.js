var HttpClient = function () {
    this.get = function (url, callback) {
        var httpRequest = new XMLHttpRequest();
        httpRequest.onreadystatechange = function () {
            if (httpRequest.readyState == 4 && httpRequest.status == 200)
                callback(httpRequest.responseText);
        }

        httpRequest.open("GET", url, true);
        httpRequest.setRequestHeader("Access-Control-Allow-Origin", "*");
        httpRequest.send(null);
    }
}

window.onload = function () {
    function UpdateStatus() {
        var client = new HttpClient();
        client.get('http://192.168.1.165:5100/api/device', function (response) {
            document.getElementById("status").innerText = response;
        });
    }

    setInterval(UpdateStatus, 300);
}