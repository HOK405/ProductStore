// $(document).ready(function(){
//     var url = "https://localhost:44384/CustomerProduct/Select";
//     $.ajax({
//         dataType: 'json',
//         url: url,
//         success: function(datas) {
//             console.log(datas);
//         }
//     });
// });

$.ajax({
    url: 'https://localhost:44384/CustomerProduct/Select',
    type: "GET",
    dataType: "json",
    data: {
    },
    success: function (result) {
        console.log(result);
    },
    error: function () {
        console.log("error");
    }
});

