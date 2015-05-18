moment.lang('zh-cn');

angular.module('tiqiu')
  .controller('LoginCtrl', ['$rootScope', '$scope', '$location', '$window', 'Auth',
    function($rootScope, $scope, $location, $window, Auth) {
      $scope.captchaUrl = domain + '/ValidateCodeHandler.ashx?t' + (+new Date());
      $scope.showCaptcha = false;

      $scope.refreshCode = function() {
        $scope.captchaUrl = domain + '/ValidateCodeHandler.ashx?t' + (+new Date());
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
          var arr = ($stateParams.fieldId || '').split('_');
          var fieldId = arr.length === 2 ? arr[0] : '';
          var field;
          $scope.fields = data.map(function(f, i) {
            var o = {
              id: f.ID,
              name: f.Name,
              active: i === 0,
              item: f.Items
            };
            if (fieldId == o.id) {
              field = o;
            }
            return o;
          });
          field = field || $scope.fields[0];
          $scope.changeField(field);
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

      $scope.buildScheduled = function () {
          Schedule.buildScheduled({fieldId : $scope.fieldItem.fieldId});
      };
      $scope.generateTimeTable = function(duration, price, start, end) {
        duration = +duration;
        price = +price;
        if (isNaN(duration) || isNaN(price) || !start || !end) {
          return;
        }

        var i = 0,
          days = [],
          weekday = ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
          period, time,
          ruleItems = [],
          fieldId = $scope.fieldItem.fieldId,
          fieldItemId = $scope.fieldItem.id,
          fieldType = $scope.fieldItem.fieldType;
        for (; i < 7; i++) {
          time = start;
          period = [];
          while (time < end && moment(time).add('hours', duration) <= end) {
            period.push({
              "StartTime": moment(time),
              "EndTime": moment(time).add('hours', duration),
              'Price': price,
              'Status': 1
            });
            time = moment(time).add('hours', duration);
          }
          days.push({
            weekday: weekday[i],
            period: period
          });
        }
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
            $scope.days = days;
          });
        
      };

      function SetModalCtrl($scope, $modalInstance, data) {
        $scope.data = {
          price: data.data.Price + '',
          start: data.data.StartTime.toDate(),
          end: data.data.EndTime.toDate(),
          status: data.data.Status == 1
        };
          $scope.ok = function() {
          if ($scope.previousEnd && moment($scope.data.start) < $scope.previousEnd) {
            return;
          }
         

          $modalInstance.close({
            Price: $scope.data.price,
            StartTime: moment($scope.data.start),
            EndTime: moment($scope.data.end),
            Status: $scope.data.status ? 1 : 0
          });
        };
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
            lastPeriod = $scope.days[dayIndex].period.slice(timeIndex+1),
            curData = $scope.days[dayIndex].period[timeIndex],
            offset = moment(data.EndTime).diff(moment(curData.EndTime),'minutes');
            
          period.push(data);

          lastPeriod.forEach(function(p,idx){
            var s = moment(p.StartTime).add('minutes',offset),
            e = moment(p.EndTime).add('minutes',offset);
           
            period.push({
              "StartTime": s,
              "EndTime": e,
              'Price': p.Price,
              'Status': p.Status
            });
            
          });


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
  .controller('BookCtrl', ['$rootScope', '$scope', '$location', '$window', 'Book', 'Customer', '$modal', '$notification', '$stateParams',
    function($rootScope, $scope, $location, $window, Book, Customer, $modal, $notification, $stateParams) {
      $scope.$on('fieldChanged', function(event, field) {
        $scope.fieldId = field.id;
        $scope.field = field;
      });

      $scope.$watch('field', function(val) {
        if (!val) return;

        var arr = ($stateParams.fieldId || '').split('_');
        var fieldItemId = arr.length === 2 ? arr[1] : '';
        var fieldItem;

        $scope.fieldItemList = val.item.map(function(item) {
          var o = {
            id: item.ID,
            title: item.Name,
            fieldId: item.FieldID
          };

          if (o.id == fieldItemId) {
            fieldItem = o;
            o.active = true;
          }
          return o;
        });

        $scope.fieldItem = fieldItem || $scope.fieldItemList[0];
      });

      $scope.$watch('fieldItem', function(val) {
        if (!val) return;

        getScheduleList();
      });

      function BookModalCtrl($scope, $modalInstance, data) {
        data.scheduledDate = moment(data.schduleItem.ScheduledDate).format('YYYY年MMMDo dddd');
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
              $modalInstance.close();
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
              $scope.name = list[0].Name;
            } else {}
          });
        };

        $scope.auditOrder = function() {
          Book.auditOrder({
            orderId: data.schduleItem.OrderID,
          })
            .then(function() {
              $modalInstance.close();
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
              $modalInstance.close();
          });
        };

        $scope.orderNormalOK = function() {
          Book.orderNormal({
            scheduledId: data.schduleItem.ScheduledID,
            phone: this.phone,
            name: this.name
          })
            .then(function() {
              $modalInstance.close();
          });
        };

        $scope.cancelOrder = function() {
          Book.cancelOrder({
            OrderID: data.schduleItem.OrderID,
            remark: this.remark
          })
            .then(function() {
              $modalInstance.close();
          });
        };

        $scope.completeOrder = function() {
          Book.completeOrder({
            OrderID: data.schduleItem.OrderID,
            income: this.income
          })
            .then(function() {
              $modalInstance.close();
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
              $modalInstance.close();
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
        },
        current = new Date(),
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
  ])
  .controller('OrderCtrl', ['$rootScope', '$scope', '$location', '$window', 'Order', 'Schedule', '$modal', 'ngTableParams', 'Book',
    function($rootScope, $scope, $location, $window, Order, Schedule, $modal, ngTableParams, Book) {
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

      function OrderModalCtrl($scope, $modalInstance, data) {
        data.scheduledDate = moment(data.schduleItem.ScheduledDate).format('YYYY年MMMDo dddd');
          $scope.data = data;
        $scope.now = new Date();

        $scope.cancel = function() {
          $modalInstance.dismiss('cancel');
        };

        $scope.auditOrder = function() {
          Book.auditOrder({
            orderId: data.item.ID || 123,
          })
            .then(function() {
              $modalInstance.close();
            }, function() {
              $modalInstance.close();
            });
        };

        $scope.cancelOrder = function() {
          Book.cancelOrder({
            OrderID: data.schduleItem.OrderID,
            remark: this.remark
          })
            .then(function() {
              $modalInstance.close();
          });
        };
      }

      $scope.openDialog = function(orderItem, dialogName) {
        var modalInstance = $modal.open({
          templateUrl: 'partial/dialog/' + dialogName + '.html?' + Date.now(),
          controller: OrderModalCtrl,
          resolve: {
            data: function() {
              return {
                item: orderItem,
                schduleItem: {
                  ScheduledDate: orderItem.OrderDate,
                  Price: orderItem.Price,
                  StartTime: orderItem.Start,
                  EndTime: orderItem.End,
                  MemberName: orderItem.MemberName,
                  MemberPhone: orderItem.MemberPhone
                },
                fieldItem: {
                  title: orderItem.FieldItemName
                },
                field: {
                  name: orderItem.FieldName
                }
              };
            }
          }

        });

        modalInstance.result.then(function(data) {
          $scope.tableParams.reload();
        }, function() {});
      };

      var mock = [{
        Type: 0,
        Status: 1,
        FieldName: '20号足球公园',
        FieldItemName: '5人一号场',
        OrderDate: '2014-07-21T10:27:37.507',
        Start: '10:00:00',
        End: '12:00:00',
        Price: 400
      }, {
        Type: 0,
        Status: 1,
        FieldName: '20号足球公园',
        FieldItemName: '5人一号场',
        OrderDate: '2014-07-21T10:27:37.507',
        Start: '10:00:00',
        End: '12:00:00',
        Price: 400
      }, {
        Type: 0,
        Status: 1,
        FieldName: '20号足球公园',
        FieldItemName: '5人一号场',
        OrderDate: '2014-07-21T10:27:37.507',
        Start: '10:00:00',
        End: '12:00:00',
        Price: 400
      }];

      $scope.tableParams = new ngTableParams({
        page: 1, // show first page
        count: 10 // count per page
      }, {
        total: 0, // length of data
        getData: function($defer, params) {
          Order.getOrderRequestList(params.page(), params.count())
            .then(function(data) {
              // update table params
              params.total(+data.Total);
              // set new data
              $defer.resolve(data.ItemList);
            });
        }
      });
    }
  ])
  .controller('PasswordCtrl', ['$rootScope', '$scope', '$stateParams', 'Customer', '$notification',
    function ($rootScope, $scope, $stateParams, Customer, $notification) {
        $scope.changepwd = function () {
            if ($scope.pwd == "" || $scope.pwd == undefined) {
                alert("请输入原密码");
                return;
            }
            if ($scope.newpwd == "" || $scope.newpwd == undefined) {
                alert("请输入新密码");
                return;
            }
            if ($scope.newpwd != $scope.renewpwd) {
                alert("两次输入的密码不一致");
                return;
            }
            Customer.changePassword({ pwd: $scope.pwd, newpwd: $scope.newpwd }).then(
                function () {
                    alert("修改成功!");
                    $scope.newpwd = "";
                    $scope.pwd = "";
                    $scope.renewpwd = "";
                }, function (msg) {
                    alert(msg.HelpMessage);
                });
        };
    }
  ]);