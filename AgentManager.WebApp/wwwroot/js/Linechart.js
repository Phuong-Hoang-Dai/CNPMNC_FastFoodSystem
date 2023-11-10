function initLineChart(revenueData) {
    var ctx = document.getElementById('revenueChart').getContext('2d');

    // Lấy tất cả các tháng
    var months = [];
    var revenues = [];
    for (var month in revenueData) {
        if (revenueData.hasOwnProperty(month)) {
            months.push(month);
            revenues.push(revenueData[month]);
        }
    }
    // // Lấy tất cả doanh thu

    // Khởi tạo biểu đồ Line Chart
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: months,
            datasets: [
                {
                    label: 'Doanh thu (VNĐ)',
                    data: revenues,
                    fill: false,
                    borderColor: 'rgb(75, 192, 192)',
                    borderWidth: 2
                }
            ]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Doanh thu (VNĐ)'
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Tháng'
                    }
                }
            }
        }
    });
}
