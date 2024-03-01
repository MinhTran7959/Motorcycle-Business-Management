//const ctx = document.getElementById('myChart');

/*const chart = require("./chart");*/

//new Chart(ctx, {
//    type: 'bar',
//    data: {
//        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
//        datasets: [{
//            label: 'Red',
//            data: [12, 19, 3, 5, 2, 3],
//            borderWidth: 1,
//             backgroundColor: [
//                 'rgba(255,99,132,0.8)',
//                 'rgba(54,162,235,0.8)',
//                 'rgba(255,206,86,0.8)',
//                 'rgba(75,192,192,0.8)',
//                 'rgba(153,102,255,0.8)',
//                 'rgba(255,159,64,0.8)'
//            ],
//        }]
//    },
//    options: {
//        scales: {
//            y: {
//                beginAtZero: true
//            },
//            x: {
//                beginAtZero: true
//            }
//        }
//    }
//});

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/GetDataChart",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
    });

    function OnSuccess(data) {
        const myChart = document.getElementById('myChart');
        /*const pieChart = document.getElementById('pieChart');*/
        var _data = data;
        var _labels = _data[0];
        var _chartData = _data[1];
        var colors = ['rgba(255, 99, 132, 0.5)',
            'rgba(255, 159, 64, 0.5)',
            'rgba(255, 205, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(201, 203, 207, 0.5)'];


        new Chart(myChart,
            {
                
                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số xe',
                        /*backgroundColor: colors,*/
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },
                ////ẩn label trong datasets
                //options: {
                //    plugins: {
                //        legend: {
                //            display: false,
                //        }
                //    }
                //}
            });
        //new Chart(pieChart,
        //    {
        //        type: 'pie',
        //        data: {
        //            labels: _labels, // Thay đổi từ label thành labels
        //            datasets: [{
        //                backgroundColor: colors,
        //                data: _chartData,
        //                borderWidth: 1,
        //            }]
        //        }
        //    });
    }
});


$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/xeDaBanMoi",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess1,
    });

    function OnSuccess1(data) {
        const myChartSale = document.getElementById('myChartSale');
        /*const pieChart = document.getElementById('pieChart');*/
        var _data = data;
        var _labels = _data[0];
        var _chartData = _data[1];
        var colors = ['rgba(255, 99, 132, 0.5)',
            'rgba(255, 159, 64, 0.5)',
            'rgba(255, 205, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(201, 203, 207, 0.5)'];


        new Chart(myChartSale,
            {

                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số xe',
                        /*backgroundColor: colors,*/
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },
                ////ẩn label trong datasets
                //options: {
                //    plugins: {
                //        legend: {
                //            display: false,
                //        }
                //    }
                //}
            });
        //new Chart(pieChart,
        //    {
        //        type: 'pie',
        //        data: {
        //            labels: _labels, // Thay đổi từ label thành labels
        //            datasets: [{
        //                backgroundColor: colors,
        //                data: _chartData,
        //                borderWidth: 1,
        //            }]
        //        }
        //    });
    }
});

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/xeTonKhoCu",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess2,
    });

    function OnSuccess2(data) {
        /*const myChart = document.getElementById('myChart');*/
        const pieChart = document.getElementById('pieChart');
        var _data = data;
        var _labels = _data[0];
        var _chartData = _data[1];
        var colors = ['rgba(255, 99, 132, 0.5)',
            'rgba(255, 159, 64, 0.5)',
            'rgba(255, 205, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(201, 203, 207, 0.5)'];


        //new Chart(myChart,
        //    {

        //        type: 'bar',
        //        data: {
        //            labels: _labels,
        //            datasets: [{
        //                label: 'số xe',
        //                /*backgroundColor: colors,*/
        //                data: _chartData,
        //                borderWidth: 1,
        //            }]
        //        },
        //        ////ẩn label trong datasets
        //        //options: {
        //        //    plugins: {
        //        //        legend: {
        //        //            display: false,
        //        //        }
        //        //    }
        //        //}
        //    });
        new Chart(pieChart,
            {
                type: 'pie',
                data: {
                    labels: _labels, // Thay đổi từ label thành labels
                    datasets: [{
                        backgroundColor: colors,
                        data: _chartData,
                        borderWidth: 1,
                    }]
                }
            });
    }
});

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/xeDaBanCu",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess3,
    });

    function OnSuccess3(data) {
        const pieChartOldSale = document.getElementById('pieChartOldSale');
        var _data = data;
        var _labels = _data[0];
        var _chartData = _data[1];
        var colors = ['rgba(255, 99, 132, 0.5)',
            'rgba(255, 159, 64, 0.5)',
            'rgba(255, 205, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(201, 203, 207, 0.5)'];

        new Chart(pieChartOldSale,
            {
                type: 'pie',
                data: {
                    labels: _labels, // Thay đổi từ label thành labels
                    datasets: [{
                        backgroundColor: colors,
                        data: _chartData,
                        borderWidth: 1,
                    }]
                }
            });
    }
});

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/xeTonKho",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess4,
    });

    function OnSuccess4(data) {
        const myChartInventoryAll = document.getElementById('myChartInventoryAll');
        /*const pieChart = document.getElementById('pieChart');*/
        var _data = data;
        var _labels = _data[0];
        var _chartData = _data[1];
        var colors = ['rgba(255, 99, 132, 0.5)',
            'rgba(255, 159, 64, 0.5)',
            'rgba(255, 205, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(201, 203, 207, 0.5)'];
        
        new Chart(myChartInventoryAll,
            {

                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số xe',
                        backgroundColor: colors,
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },
                
            });
        
    }
});


$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/xeDaBan",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess5,
    });

    function OnSuccess5(data) {
        const myChartSaleAll = document.getElementById('myChartSaleAll');
        /*const pieChart = document.getElementById('pieChart');*/
        var _data = data;
        var _labels = _data[0];
        var _chartData = _data[1];
        var colors = ['rgba(255, 99, 132, 0.5)',
            'rgba(255, 159, 64, 0.5)',
            'rgba(255, 205, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(153, 102, 255, 0.5)',
            'rgba(201, 203, 207, 0.5)'];

        new Chart(myChartSaleAll,
            {

                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số xe',
                        backgroundColor: colors,
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },

            });
    }
});




$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/TienChiTheoThang",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess6,
    });
    function OnSuccess6(data) {
        const myLine = document.getElementById('myLine');
        //const labels = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"];
        //const _chartData = data;

        //new Chart(myLine, {
        //    labels: labels,
        //    type: 'line',
        //    data: {
        //        datasets: [{
        //            label: 'Danh thu theo tháng',
        //            data: _chartData,
        //            fill: true,
        //            backgroundColor: 'rgba(255, 159, 64, 0.5)',
        //            borderColor: 'rgb(75, 192, 192)',
        //            tension: 0.1,
        //            borderWidth: 1,
        //        }]
        //    }
        //});
        var _labels = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"];
        const _chartData = data;

        new Chart(myLine,
            {
                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số tiền',
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },

            });
    }
});


$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/TienThuTheoThang",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess7,
    });
    function OnSuccess7(data) {
        const myLineThu = document.getElementById('myLineThu');
        var _labels = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"];
        const _chartData = data;

        new Chart(myLineThu,
            {
                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số tiền',
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },

            });
    }
});

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/TienChiTheoNam",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess8,
    });
    function OnSuccess8(data) {
        const myBarChiNam = document.getElementById('myBarChiNam');
        var _labels = ["2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030", "2031", "2032", "2033"];
        const _chartData = data;

        new Chart(myBarChiNam,
            {
                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số tiền',
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },

            });
    }
});
$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/TienThuTheoNam",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess9,
    });
    function OnSuccess9(data) {
        const myBarThuNam = document.getElementById('myBarThuNam');
        var _labels = ["2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030", "2031", "2032", "2033"];
        const _chartData = data;

        new Chart(myBarThuNam,
            {
                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số tiền',
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },

            });
    }
});

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Chitietxes/LoiNhuanTheoNam",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess10,
    });
    function OnSuccess10(data) {
        const myBarLoiNhuanNam = document.getElementById('myBarLoiNhuanNam');
        var _labels = ["2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030", "2031", "2032", "2033"];
        const _chartData = data;

        new Chart(myBarLoiNhuanNam,
            {
                type: 'bar',
                data: {
                    labels: _labels,
                    datasets: [{
                        label: 'số tiền',
                        data: _chartData,
                        borderWidth: 1,
                    }]
                },

            });
    }
});