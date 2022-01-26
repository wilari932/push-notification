(function () {


    function createHubClient() {

        var connection = new signalR.HubConnectionBuilder()
            .withUrl("/notificationshub")
            .withAutomaticReconnect({
                nextRetryDelayInMilliseconds: retryContext => {
                    if (retryContext.elapsedMilliseconds < 60000) {
                        // If we've been reconnecting for less than 60 seconds so far,
                        // wait between 0 and 10 seconds before the next reconnect attempt.
                        return Math.random() * 10000;
                    } else {
                        // If we've been reconnecting for more than 60 seconds so far, stop reconnecting.
                        return null;
                    }
                }
            })
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.start().then(con => {
            connection.on("Perros", function (message) {
                console.log(message);
                new Notification(message,
                    {
                        body: "Hello",
                        vibrate: [200, 100, 200],
                        image:"/whatsapp.png"
                    });
            });
        });

    }



    window.Notification.requestPermission().then(function (permission) {
        if (permission == 'granted') {
            createHubClient();
        }
    });




})();
