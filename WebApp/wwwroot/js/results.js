document.addEventListener('DOMContentLoaded', function () {
    // Function to create a bar chart
    function createBarChart(ctx, labels, data) {
        const chartData = {
            labels: labels,
            datasets: [{
                label: 'تعداد پاسخ‌ها',
                data: data,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        };

        const config = {
            type: 'bar',
            data: chartData,
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        };

        new Chart(ctx, config);
    }

    // Function to create a pie chart
    function createPieChart(ctx, labels, data) {
        const chartData = {
            labels: labels,
            datasets: [{
                label: 'تعداد افراد',
                data: data,
                backgroundColor: ['rgba(75, 192, 192, 0.2)', 'rgba(255, 99, 132, 0.2)'],
                borderColor: ['rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)'],
                borderWidth: 1
            }]
        };

        const config = {
            type: 'pie',
            data: chartData,
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'پاسخ‌دهنده‌ها در مقابل غیر پاسخ‌دهنده‌ها'
                    }
                }
            }
        };

        new Chart(ctx, config);
    }

    // Initialize bar charts
    document.querySelectorAll('canvas[data-chart-type="bar"]').forEach(canvas => {
        const labels = JSON.parse(canvas.getAttribute('data-labels'));
        const data = JSON.parse(canvas.getAttribute('data-data'));
        const ctx = canvas.getContext('2d');
        createBarChart(ctx, labels, data);
    });

    // Initialize pie chart
    const pieChartCanvas = document.getElementById('respondentsChart');
    if (pieChartCanvas) {
        const labels = JSON.parse(pieChartCanvas.getAttribute('data-labels'));
        const data = JSON.parse(pieChartCanvas.getAttribute('data-data'));
        const ctx = pieChartCanvas.getContext('2d');
        createPieChart(ctx, labels, data);
    }
});
