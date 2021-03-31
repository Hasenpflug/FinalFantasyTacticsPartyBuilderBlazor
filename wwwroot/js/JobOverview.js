var JobOverview = function () {
    var modifierChartCanvas, modifierChart, growthChartCanvas, growthChart, baseMoveJumpChartCanvas, baseMoveJumpChart, baseEvasionChartCanvas, baseEvasionChart,
        selectedJobData;

    function updateChartData(jobData) {
        selectedJobData = JSON.parse(jobData);
        document.getElementById('job-name').innerHTML = selectedJobData.DisplayName;
        modifierChart.data.datasets[0].data = [parseInt(selectedJobData.HPMultiplier), parseInt(selectedJobData.MPMultiplier), parseInt(selectedJobData.SpeedMulitplier),
            parseInt(selectedJobData.PhysicalAttackMultiplier), parseInt(selectedJobData.MagicalAttackMultiplier)];
        growthChart.data.datasets[0].data = [parseInt(selectedJobData.HPGrowthConstant), parseInt(selectedJobData.MPGrowthConstant), parseInt(selectedJobData.SpeedGrowthConstant),
            parseInt(selectedJobData.PhysicalAttackGrowthConstant), parseInt(selectedJobData.MagicalAttackGrowthConstant)];
        baseMoveJumpChart.data.datasets[0].data = [parseInt(selectedJobData.BaseMoveLength), parseInt(selectedJobData.BaseJumpHeight)];
        baseEvasionChart.data.datasets[0].data = [parseInt(selectedJobData.BaseCombatEvasion)];
        modifierChart.update();
        growthChart.update();
        baseMoveJumpChart.update();
        baseEvasionChart.update();
    }

    function initializeJobModifierChart() {
        modifierChartCanvas = document.getElementById('job-modifier-chart');
        modifierChart = new Chart(modifierChartCanvas, {
            type: 'radar',
            data: {
                labels: ["HPM", "MPM", "SPM", "PAM", "MAM"],
                datasets: [{
                    label: 'Job Stat Modifiers',
                    data: [100, 75, 100, 90, 80],
                    borderColor: 'rgba(255, 0, 0, 0.5)',
                    backgroundColor: 'rgba(255, 0, 0, 0.34)',
                    pointBackgroundColor: 'white',
                    pointBorderColor: 'yellow',
                    defaultFontFamily: 'Altima',
                    defaultFontSize: '20'
                }]
            },
            options: {
                maintainAspectRatio: false,
                scale: {
                    ticks: {
                        suggestedMin: 40,
                        suggestedMax: 160,
                        stepSize: 10,
                        display: false
                    },
                    pointLabels: {
                        fontSize: window.innerWidth > 600 ? 60 : 30,
                        fontFamily: 'Altima'
                    }
                },
                legend: false
            }
        });

        modifierChart.resize();
    }

    function initializeJobGrowthChart(data) {
        growthChartCanvas = document.getElementById('job-growth-chart');
        growthChart = new Chart(growthChartCanvas, {
            type: 'radar',
            data: {
                labels: ["HPC", "MPC", "SPC", "PAC", "MAC"],
                datasets: [{
                    label: 'Job Stat Constants',
                    data: [75, 76, 80, 68, 80],
                    borderColor: 'rgba(255, 0, 0, 0.5)',
                    backgroundColor: 'rgba(255, 0, 0, 0.34)',
                    pointBackgroundColor: 'white',
                    pointBorderColor: 'yellow',
                    defaultFontFamily: 'Altima',
                    defaultFontSize: '60'
                }]
            },
            options: {
                maintainAspectRatio: false,
                scale: {
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 100,
                        stepSize: 10,
                        display: false
                    },
                    pointLabels: {
                        fontSize: window.innerWidth > 600 ? 60 : 30,
                        fontFamily: 'Altima'
                    }
                },
                legend: false,
            }
        });

        growthChart.resize();
    }

    function initializeJobMoveChart(data) {
        baseMoveJumpChartCanvas = document.getElementById('job-move-chart');
        baseMoveJumpChart = new Chart(baseMoveJumpChartCanvas, {
            type: 'bar',
            data: {
                labels: ["Move", "Jump"],
                datasets: [{
                    label: 'Job Move Attributes',
                    data: [4, 3],
                    backgroundColor: 'rgba(255, 0, 0, 0.34)'
                }]
            },
            options: {
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: 4,
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontSize: window.innerWidth > 600 ? 40 : 30,
                            fontFamily: 'Altima'
                        }
                    }]
                },
                legend: false
            }
        });

        baseMoveJumpChart.resize();
    }

    function initializeJobEvasionChart() {
        baseEvasionChartCanvas = document.getElementById('job-evasion-chart');
        baseEvasionChart = new Chart(baseEvasionChartCanvas, {
            type: 'bar',
            data: {
                labels: ["Evasion"],
                datasets: [{
                    label: 'Job Base Evasion',
                    data: [5],
                    backgroundColor: 'rgba(255, 0, 0, 0.34)'
                }]
            },
            options: {
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: 30,
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontSize: window.innerWidth > 600 ? 40 : 30,
                            fontFamily: 'Altima'
                        }
                    }]
                },
                legend: false
            }
        });

        baseEvasionChart.resize();
    }

    function getModifierChart() {
        return modifierChart;
    }

    return {
        getModifierChart: getModifierChart,
        initializeJobModifierChart: initializeJobModifierChart,
        initializeJobGrowthChart: initializeJobGrowthChart,
        initializeJobMoveChart: initializeJobMoveChart,
        initializeJobEvasionChart: initializeJobEvasionChart,
        updateChartData: updateChartData
    }
}();