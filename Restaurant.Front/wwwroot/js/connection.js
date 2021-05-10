var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

console.log("a")

connection.start()
    .then(result => {
        connection.invoke("TestConnection").catch(function (err) {
            return console.error(err.toString());
        });
    });

export default connection;