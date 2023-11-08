var myBarChart; // Biến toàn cục để lưu biểu đồ

function initChart(orderCounts) {
    var ctx = document.getElementById('myBarChart').getContext('2d');

    if (myBarChart) {
        myBarChart.destroy(); // Hủy bỏ biểu đồ cũ nếu tồn tại
    }

    myBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: Object.keys(orderCounts), // Nhãn của cột x
            datasets: [{
                label: 'Số lượng đơn', // Nhãn của dữ liệu
                data: Object.values(orderCounts), // Dữ liệu cột y
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
