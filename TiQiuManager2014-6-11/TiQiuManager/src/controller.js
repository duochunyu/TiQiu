moment.lang('zh-cn');

angular.module('tiqiu')
    .controller('LoginCtrl', ['$rootScope', '$scope', '$location', '$window', 'Auth',
        function($rootScope, $scope, $location, $window, Auth) {
            $scope.captchaUrl = 'http://api.tiqiu365.com/ValidateCodeHandler.ashx?t' + (+new Date());
            $scope.showCaptcha = false;

            $scope.refreshCode = function() {
                $scope.captchaUrl = 'http://api.tiqiu365.com/ValidateCodeHandler.ashx?t' + (+new Date());
            };

            $scope.login = function() {
                Auth.login({
                    username: $scope.username,
                    password: $scope.password,
                    code: $scope.code
                })
                    .then(function() {
                        $location.path('/book/');
                    }, function(response) {
                        $scope.error = response.HelpMessage;
                        if (response.HelpMessage == '你已输入错误3次用户名密码，请仔细核对并填写验证码！') {
                            $scope.showCaptcha = true;
                            $scope.refreshCode();
                        }
                    });
            };
        }
    ])
    .controller('SidebarCtrl', ['$rootScope', '$scope', '$stateParams', 'Book',
        function($rootScope, $scope, $stateParams, Book) {
            // todo: 不用每次取了
            Book.getFieldItemList()
                .then(function(data) {
                    $scope.fields = data.map(function(f, i) {
                        return {
                            id: f.ID,
                            name: f.Name,
                            active: i === 0,
                            item: f.Items
                        }
                    });
                    $scope.changeField($scope.fields[0]);
                });

            $scope.changeField = function(field) {
                $scope.fieldId = field.id;
                $scope.fields.forEach(function(f) {
                    if (f.id == field.id) {
                        f.active = true;
                    } else {
                        f.active = false;
                    }
                });

                $scope.$emit('fieldChanged', field);
            };
        }
    ])
    .controller('ConfigCtrl', ['$rootScope', '$scope', '$location', '$window', 'Book', 'Schedule', '$modal',
        function($rootScope, $scope, $location, $window, Book, Schedule, $modal) {
            $scope.$on('fieldChanged', function(event, field) {
                $scope.fieldId = field.id;
                $scope.field = field;
            });

            $scope.$watch('field', function(val) {
                if (!val) return;

                $scope.fieldItemList = val.item.map(function(item) {
                    return {
                        id: item.ID,
                        title: item.Name,
                        fieldId: item.FieldID,
                        fieldType: item.FieldType
                    };
                });

                $scope.fieldItem = $scope.fieldItemList[0];
            });

            $scope.$watch('fieldItem', function(val) {
                if (!val) return;

                Schedule.getFieldRuleList({
                    fieldItemId: val.id
                }).then(function(data) {
                    var days = ['周一', '周二', '周三', '周四', '周五', '周六', '周日'].map(function(val) {
                        return {
                            weekday: val,
                            period: []
                        };
                    });

                    data.forEach(function(item) {
                        days[item.DayOfWeek - 1].period.push({
                            "StartTime": moment(moment().format('YYYY MMDD ') + item.Start, 'YYYY MMDD HH:mm:ss'),
                            "EndTime": moment(moment().format('YYYY MMDD ') + item.End, 'YYYY MMDD HH:mm:ss'),
                            'Price': item.Price,
                            'Status': item.Status
                        });
                    });

                    $scope.days = days;
                });
            });

            $scope.switchFieldItem = function(item) {
                $scope.fieldItem = item;
            };

            var d = new Date();
            d.setHours(9);
            d.setMinutes(0);
            $scope.start = d;
            d = new Date();
            d.setHours(23);
            d.setMinutes(0);
            $scope.end = d;
            $scope.duration = 1.5;
            $scope.price = 300;

            $scope.generateTimeTable = function(duration, price, start, end) {
                duration = +duration;
                price = +price;
                if (isNaN(duration) || isNaN(price) || !start || !end) {
                    return;
                }

                var i = 0,
                    days = [],
                    weekday = ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
                    period, time;
                for (; i < 7; i++) {
                    time = start;
                    period = [];
                    while (time < end) {
                        period.push({
                            "StartTime": moment(time),
                            "EndTime": moment(time).add('hours', duration),
                            'Price': price,
                            'Status': 0
                        });
                        time = moment(time).add('hours', duration);
                    }
                    days.push({
                        weekday: weekday[i],
                        period: period
                    })
                }
                $scope.days = days;
            };

            function SetModalCtrl($scope, $modalInstance, data) {
                $scope.data = {
                    price: data.data.Price + '',
                    start: data.data.StartTime.toDate(),
                    end: data.data.EndTime.toDate(),
                    status: data.data.Status == 0
                }

                $scope.ok = function() {
                    if ($scope.previousEnd && moment($scope.data.start) < $scope.previousEnd) {
                        return;
                    }

                    $modalInstance.close({
                        Price: $scope.data.price,
                        StartTime: moment($scope.data.start),
                        EndTime: moment($scope.data.end),
                        Status: $scope.data.status ? 0 : 1
                    });
                }

                $scope.cancel = function() {
                    $modalInstance.dismiss('cancel');
                };
            }

            $scope.openDialog = function(dayIndex, timeIndex) {
                var modalInstance = $modal.open({
                    templateUrl: 'partial/dialog/SetSchedule.html?' + Date.now(),
                    controller: SetModalCtrl,
                    resolve: {
                        data: function() {
                            return {
                                data: $scope.days[dayIndex]['period'][timeIndex],
                                previousEnd: timeIndex == 0 ? null : $scope.days[dayIndex]['period'][timeIndex - 1]['EndTime']
                            };
                        }
                    }
                });

                modalInstance.result.then(function(data) {
                    var period = $scope.days[dayIndex].period.slice(0, timeIndex),
                        time = data.EndTime;
                    period.push(data);

                    while (time < $scope.end) {
                        period.push({
                            "StartTime": moment(time),
                            "EndTime": moment(time).add('hours', $scope.duration),
                            'Price': $scope.price,
                            'Status': 0
                        });
                        time = moment(time).add('hours', $scope.duration);
                    }

                    var days = $scope.days.slice(0),
                        ruleItems = [],
                        fieldId = $scope.fieldItem.fieldId,
                        fieldItemId = $scope.fieldItem.id,
                        fieldType = $scope.fieldItem.fieldType;
                    days[dayIndex] = {
                        weekday: days[dayIndex].weekday,
                        period: period
                    };

                    days.forEach(function(day, index) {
                        day.period.forEach(function(p) {
                            ruleItems.push({
                                FieldId: fieldId,
                                FieldType: fieldType,
                                Start: p.StartTime.format('HH:mm:ss'),
                                End: p.EndTime.format('HH:mm:ss'),
                                Price: p.Price,
                                DayOfWeek: index + 1,
                                Status: p.Status
                            });
                        });
                    });

                    Schedule.createFieldRuleList({
                        fieldItemId: fieldItemId,
                        ruleItems: ruleItems
                    }).then(function(data) {
                        $scope.days[dayIndex].period = period;
                    });
                }, function() {});
            };
        }
    ])
    .controller('BookCtrl', ['$rootScope', '$scope', '$location', '$window', 'Book', 'Customer', '$modal', '$notification',
        function($rootScope, $scope, $location, $window, Book, Customer, $modal, $notification) {
            $scope.$on('fieldChanged', function(event, field) {
                $scope.fieldId = field.id;
                $scope.field = field;
            });

            setTimeout(function() {
                $notification.success('提醒', '您有新的预定<a>点击查看</a>');
            }, 500);

            $scope.$watch('field', function(val) {
                if (!val) return;

                $scope.fieldItemList = val.item.map(function(item) {
                    return {
                        id: item.ID,
                        title: item.Name,
                        fieldId: item.FieldID
                    };
                });

                $scope.fieldItem = $scope.fieldItemList[0];
            });

            $scope.$watch('fieldItem', function(val) {
                if (!val) return;

                getScheduleList();
            });

            function BookModalCtrl($scope, $modalInstance, data) {
                data.scheduledDate = moment(data.schduleItem.ScheduledDate).format('YYYY年MMMDo dddd')
                $scope.data = data;
                $scope.now = new Date();
                $scope.ok = function() {
                    Book.orderFreeTeam({
                        minPlayerCount: this.minPlayerCount,
                        priceUnit: this.priceUnit,
                        price: this.price,
                        scheduledId: data.schduleItem.ScheduledID
                    })
                        .then(function() {
                            $modalInstance.close()
                        });
                };

                $scope.cancel = function() {
                    $modalInstance.dismiss('cancel');
                };
                $scope.findMember = function() {
                    Customer.getMemberList({
                        phone: this.phone
                    }).then(function(list) {
                        if (list != null && list.length == 1) {
                            $scope.name = list[0].Name
                        } else {}
                    });
                };

                $scope.auditOrder = function() {
                    Book.auditOrder({
                        orderId: data.schduleItem.OrderID,
                    })
                        .then(function() {
                            $modalInstance.close()
                        });
                };

                $scope.batchOrderByManager = function() {
                    Book.batchOrder({
                        scheduledId: data.schduleItem.ScheduledID,
                        phone: this.phone,
                        name: this.name,
                        start: moment().format('YYYY-MM-DD'),
                        end: moment(this.endDate).format('YYYY-MM-DD')
                    })
                        .then(function() {
                            $modalInstance.close()
                        });
                };

                $scope.orderNormalOK = function() {
                    Book.orderNormal({
                        scheduledId: data.schduleItem.ScheduledID,
                        phone: this.phone,
                        name: this.name
                    })
                        .then(function() {
                            $modalInstance.close()
                        });
                };

                $scope.cancelOrder = function() {
                    Book.cancelOrder({
                        OrderID: data.schduleItem.OrderID,
                        remark: this.remark
                    })
                        .then(function() {
                            $modalInstance.close()
                        });
                };

                $scope.completeOrder = function() {
                    Book.completeOrder({
                        OrderID: data.schduleItem.OrderID,
                        income: this.income
                    })
                        .then(function() {
                            $modalInstance.close()
                        });
                };

                $scope.joinFreeTeam = function() {
                    Book.joinFreeTeam({
                        OrderID: data.schduleItem.OrderID,
                        playerCount: this.playerCount,
                        phone: this.phone,
                        name: this.name
                    })
                        .then(function() {
                            $modalInstance.close()
                        });
                };
            }

            $scope.openDialog = function(schduleItem, dialogName) {
                var modalInstance = $modal.open({
                    templateUrl: 'partial/dialog/' + dialogName + '.html?' + Date.now(),
                    controller: BookModalCtrl,
                    resolve: {
                        data: function() {
                            return {
                                schduleItem: schduleItem,
                                fieldItem: $scope.fieldItem,
                                field: $scope.field
                            };
                        }
                    }

                });

                modalInstance.result.then(function(data) {
                    getScheduleList();
                }, function() {});
            };
            var statusMap = {
                '0': 'Available',
                '1': 'Booking',
                '2': 'Booked',
                '3': 'CustomerConfirmed',
                '10': 'CheckIn',
                '20': 'Ending',
                '50': 'Expired',
                '60': 'Canceled',
                '-1': 'Void'
            }, current = new Date(),
                day = current.getDay(),
                date = ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
                start, end;
            start = moment(current).subtract('days', day - 1).format('YYYY-MM-DD');
            end = moment(current).add('days', 7 - day).format('YYYY-MM-DD');

            $scope.switchFieldItem = function(item) {
                $scope.fieldItem = item;
            };

            $scope.query = function(dir) {
                if (dir === 'prev') {
                    start = moment(Date.parse(start)).subtract('days', 7).format('YYYY-MM-DD');
                    end = moment(Date.parse(end)).subtract('days', 7).format('YYYY-MM-DD');
                } else {
                    start = moment(Date.parse(start)).add('days', 7).format('YYYY-MM-DD');
                    end = moment(Date.parse(end)).add('days', 7).format('YYYY-MM-DD');
                }

                getScheduleList();
            };

            function getScheduleList() {
                var dateList = [],
                    i;
                for (i = 0; i < 7; i++) {
                    dateList.push(moment(Date.parse(start)).add('days', i).format('YYYY-MM-DD'));
                }

                Book.getFieldItemScheduledList($scope.fieldItem, start, end)
                    .then(function(data) {
                        var curNow = new Date();
                        var cur = moment(curNow).format("YYYY-MM-DD").slice(0, 10) + moment(curNow).format("HH:mm");
                        data.forEach(function(d) {
                            d.Status = statusMap[d.OrderStatus + ''];
                            var isExpire = d.ScheduledDate.slice(0, 10) + d.StartTime.slice(0, 5) < cur;
                            d.isExpire = isExpire;
                            if (isExpire && d.OrderStatus == 0) d.Status = 'Void';
                        });

                        var map = _.groupBy(data, function(d) {
                            return d.ScheduledDate.slice(0, 10);
                        }),
                            days = [];

                        i = 0;
                        for (i = 0; i < 7; i++) {
                            days.push({
                                weekday: date[i],
                                day: moment(dateList[i]).format('MMMDo'),
                                period: map[dateList[i]] || [{
                                    "Status": 'Void',
                                    "StartTime": "09:00:00",
                                    "EndTime": "10:30:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "09:00" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "10:30:00",
                                    "EndTime": "12:00:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "10:30" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "12:00:00",
                                    "EndTime": "13:30:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "12:00" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "13:30:00",
                                    "EndTime": "15:00:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "13:30" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "15:00:00",
                                    "EndTime": "16:30:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "015:00" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "16:30:00",
                                    "EndTime": "18:00:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "16:30" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "18:00:00",
                                    "EndTime": "19:30:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "18:00" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "19:30:00",
                                    "EndTime": "21:00:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "19:30" < cur
                                }, {
                                    "Status": 'Void',
                                    "StartTime": "21:00:00",
                                    "EndTime": "22:30:00",
                                    "isExpire": moment(dateList[i]).format('YYYY-MM-DD').slice(0, 10) + "21:00" < cur
                                }]
                            });
                        }

                        $scope.days = days;
                    });
            }
        }
    ]);