angular.module('tiqiu')
    .directive('toggleSwitch', function() {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                model: '=',
                disabled: '@',
                onLabel: '@',
                offLabel: '@',
                knobLabel: '@'
            },
            template: '<div class="switch" ng-click="toggle()" ng-class="{ \'disabled\': disabled }"><div class="switch-animate" ng-class="{\'switch-off\': !model, \'switch-on\': model}"><span class="switch-left" ng-bind="onLabel"></span><span class="knob" ng-bind="knobLabel"></span><span class="switch-right" ng-bind="offLabel"></span></div></div>',
            controller: function($scope) {
                $scope.toggle = function toggle() {
                    if (!$scope.disabled) {
                        $scope.model = !$scope.model;
                    }
                };
            },
            compile: function(element, attrs) {
                if (!attrs.onLabel) {
                    attrs.onLabel = 'On';
                }
                if (!attrs.offLabel) {
                    attrs.offLabel = 'Off';
                }
                if (!attrs.knobLabel) {
                    attrs.knobLabel = '\u00a0';
                }
                if (!attrs.disabled) {
                    attrs.disabled = false;
                }
            },
        };
    })
    .factory('$notification', ['$timeout', '$sce',
        function($timeout, $sce) {

            console.log('notification service online');
            var notifications = JSON.parse(localStorage.getItem('$notifications')) || [],
                queue = [];

            var settings = {
                info: {
                    duration: 5000,
                    enabled: true
                },
                warning: {
                    duration: 5000,
                    enabled: true
                },
                error: {
                    duration: 5000,
                    enabled: true
                },
                success: {
                    duration: 5000,
                    enabled: true
                },
                progress: {
                    duration: 0,
                    enabled: true
                },
                custom: {
                    duration: 35000,
                    enabled: true
                },
                details: true,
                localStorage: false,
                html5Mode: false,
                html5DefaultIcon: 'icon.png'
            };

            function html5Notify(icon, title, content, ondisplay, onclose) {
                if (window.webkitNotifications.checkPermission() === 0) {
                    if (!icon) {
                        icon = 'favicon.ico';
                    }
                    var noti = window.webkitNotifications.createNotification(icon, title, content);
                    if (typeof ondisplay === 'function') {
                        noti.ondisplay = ondisplay;
                    }
                    if (typeof onclose === 'function') {
                        noti.onclose = onclose;
                    }
                    noti.show();
                } else {
                    settings.html5Mode = false;
                }
            }


            return {

                /* ========== SETTINGS RELATED METHODS =============*/

                disableHtml5Mode: function() {
                    settings.html5Mode = false;
                },

                disableType: function(notificationType) {
                    settings[notificationType].enabled = false;
                },

                enableHtml5Mode: function() {
                    // settings.html5Mode = true;
                    settings.html5Mode = this.requestHtml5ModePermissions();
                },

                enableType: function(notificationType) {
                    settings[notificationType].enabled = true;
                },

                getSettings: function() {
                    return settings;
                },

                toggleType: function(notificationType) {
                    settings[notificationType].enabled = !settings[notificationType].enabled;
                },

                toggleHtml5Mode: function() {
                    settings.html5Mode = !settings.html5Mode;
                },

                requestHtml5ModePermissions: function() {
                    if (window.webkitNotifications) {
                        console.log('notifications are available');
                        if (window.webkitNotifications.checkPermission() === 0) {
                            return true;
                        } else {
                            window.webkitNotifications.requestPermission(function() {
                                if (window.webkitNotifications.checkPermission() === 0) {
                                    settings.html5Mode = true;
                                } else {
                                    settings.html5Mode = false;
                                }
                            });
                            return false;
                        }
                    } else {
                        console.log('notifications are not supported');
                        return false;
                    }
                },


                /* ============ QUERYING RELATED METHODS ============*/

                getAll: function() {
                    // Returns all notifications that are currently stored
                    return notifications;
                },

                getQueue: function() {
                    return queue;
                },

                /* ============== NOTIFICATION METHODS ==============*/

                info: function(title, content, userData) {
                    console.log(title, content);
                    return this.awesomeNotify('info', 'info', title, content, userData);
                },

                error: function(title, content, userData) {
                    return this.awesomeNotify('error', 'remove', title, content, userData);
                },

                success: function(title, content, userData) {
                    return this.awesomeNotify('success', 'ok', title, content, userData);
                },

                warning: function(title, content, userData) {
                    return this.awesomeNotify('warning', 'exclamation', title, content, userData);
                },

                awesomeNotify: function(type, icon, title, content, userData) {
                    /**
                     * Supposed to wrap the makeNotification method for drawing icons using font-awesome
                     * rather than an image.
                     *
                     * Need to find out how I'm going to make the API take either an image
                     * resource, or a font-awesome icon and then display either of them.
                     * Also should probably provide some bits of color, could do the coloring
                     * through classes.
                     */
                    // image = '<i class="icon-' + image + '"></i>';
                    return this.makeNotification(type, false, icon, title, content, userData);
                },

                notify: function(image, title, content, userData) {
                    // Wraps the makeNotification method for displaying notifications with images
                    // rather than icons
                    return this.makeNotification('custom', image, true, title, content, userData);
                },

                makeNotification: function(type, image, icon, title, content, userData) {
                    var notification = {
                        'type': type,
                        'image': image,
                        'icon': icon,
                        'title': title,
                        'content': $sce.trustAsHtml(content),
                        'timestamp': +new Date(),
                        'userData': userData
                    };
                    notifications.push(notification);

                    if (settings.html5Mode) {
                        html5Notify(image, title, content, function() {
                            console.log("inner on display function");
                        }, function() {
                            console.log("inner on close function");
                        });
                    } else {
                        queue.push(notification);
                        $timeout(function removeFromQueueTimeout() {
                            queue.splice(queue.indexOf(notification), 1);
                        }, settings[type].duration);

                    }

                    this.save();
                    return notification;
                },


                /* ============ PERSISTENCE METHODS ============ */

                save: function() {
                    // Save all the notifications into localStorage
                    // console.log(JSON);
                    if (settings.localStorage) {
                        localStorage.setItem('$notifications', JSON.stringify(notifications));
                    }
                    // console.log(localStorage.getItem('$notifications'));
                },

                restore: function() {
                    // Load all notifications from localStorage
                },

                clear: function() {
                    notifications = [];
                    this.save();
                }

            };
        }
    ])
    .directive('notifications', ['$notification', '$compile',
        function($notification, $compile) {
            /**
             *
             * It should also parse the arguments passed to it that specify
             * its position on the screen like "bottom right" and apply those
             * positions as a class to the container element
             *
             * Finally, the directive should have its own controller for
             * handling all of the notifications from the notification service
             */
            console.log('this is a new directive');
            var html =
                '<div class="dr-notification-wrapper" ng-repeat="noti in queue">' +
                '<div class="dr-notification-close-btn" ng-click="removeNotification(noti)">' +
                '<i class="icon-remove"></i>' +
                '</div>' +
                '<div class="dr-notification">' +
                '<div class="dr-notification-image dr-notification-type-{{noti.type}}" ng-switch on="noti.image">' +
                '<i class="icon-{{noti.icon}}" ng-switch-when="false"></i>' +
                '<img ng-src="{{noti.image}}" ng-switch-default />' +
                '</div>' +
                '<div class="dr-notification-content">' +
                '<h3 class="dr-notification-title">{{noti.title}}</h3>' +
                '<p class="dr-notification-text" ng-bind-html="noti.content"></p>' +
                '</div>' +
                '</div>' +
                '</div>';


            function link(scope, element, attrs) {
                var position = attrs.notifications;
                position = position.split(' ');
                element.addClass('dr-notification-container');
                for (var i = 0; i < position.length; i++) {
                    element.addClass(position[i]);
                }
            }


            return {
                restrict: 'A',
                scope: {},
                template: html,
                link: link,
                controller: ['$scope',
                    function NotificationsCtrl($scope) {
                        $scope.queue = $notification.getQueue();

                        $scope.removeNotification = function(noti) {
                            $scope.queue.splice($scope.queue.indexOf(noti), 1);
                        };
                    }
                ]

            };
        }
    ])
    .directive('bookMenu', ['$document', '$compile', '$position',
        function($document, $compile, $position) {
            return {
                restrict: 'EA',
                scope: true,
                compile: function(tElem, tAttrs) {
                    var template = '<div class="popover left in">' +
                        '<div class="arrow"></div>' +
                        '<div class="popover-content">' +
                        '<button class="btn btn-default btn-block btn-xs" ng-repeat="option in options" ng-click="openDialog(option.type)">{{option.name}}</button>' +
                        '</div>' +
                        '</div>',
                        allOptions = [{
                            name: '发布预定',
                            type: 'OrderNormalByManager'
                        }, {
                            name: '发布单飞',
                            type: 'OrderFreeTeam'
                        }, {
                            name: '约战',
                            type: 'PK'
                        }, {
                            name: '批量预定',
                            type: 'BatchOrderByManager'
                        }, {
                            name: '加入单飞',
                            type: 'joinFreeTeam'
                        }, {
                            name: '取消订单',
                            type: 'cancelOrder'
                        }, {
                            name: '审核预定',
                            type: 'AuditOrder'
                        }];

                    return function link(scope, element, attrs) {
                        var tooltip = $compile(template)(scope);
                        element.after(tooltip);

                        attrs.$observe('bookMenu', function(val) {
                            scope.scheduleItem = JSON.parse(val);

                            if (scope.scheduleItem.ScheduledID) {
                                var isExpire = scope.scheduleItem.ScheduledDate.slice(0, 10) + scope.scheduleItem.StartTime.slice(0, 5) < moment().format("YYYY-MM-DD").slice(0, 10) + moment().format("HH:mm"),
                                    options = [];
                                if (isExpire) {
                                    scope.scheduleItem.OrderStatus = '50';
                                }
                            } else {
                                scope.scheduleItem = '-1';
                            }
                            switch (scope.scheduleItem.OrderStatus + '') {
                                case '0':
                                    options = [0, 1, 3];
                                    break;
                                case '1':
                                    if (scope.scheduleItem.OrderType == 2) {
                                        options = [4, 5, 6];
                                    } else {
                                        options = [5, 6];
                                    }
                                    break;
                                case '2':
                                case '3':
                                    if (scope.scheduleItem.OrderType == 2) {
                                        options = [4, 5];
                                    } else {
                                        options = [5];
                                    }
                                    break;
                                case '10':
                                case '20':
                                case '50':
                                case '60':
                                case '-1':
                                    break;
                                default:
                                    options = [0, 1, 3];
                                    break;
                            }

                            scope.options = options.map(function(index) {
                                return allOptions[index];
                            });
                        });

                        scope.openDialog = function(type) {
                            scope.$parent.openDialog(scope.scheduleItem, type);
                            scope.showMe = false;
                        };

                        function positionTooltip() {
                            var position,
                                ttWidth,
                                ttHeight,
                                ttPosition;
                            // Get the position of the directive element.
                            position = $position.position(element);

                            // Get the height and width of the tooltip so we can center it.
                            ttWidth = tooltip.prop('offsetWidth');
                            ttHeight = tooltip.prop('offsetHeight');

                            ttPosition = {
                                top: position.top + position.height / 2 - ttHeight / 2,
                                left: position.left + element.prop('offsetWidth') / 2 - ttWidth
                            };

                            ttPosition.top += 'px';
                            ttPosition.left += 'px';

                            // Now set the calculated positioning.
                            tooltip.css(ttPosition);
                        };

                        function toggle() {
                            if (!scope.options.length) {
                                scope.showMe = false;
                                return;
                            }

                            scope.showMe = !scope.showMe;
                            scope.$digest();
                        }

                        // todo: 
                        $document.bind('click', function(event) {
                            var isClickedElementChildOfPopup = element[0] === event.target || element
                                .find(event.target)
                                .length > 0;

                            if (isClickedElementChildOfPopup && scope.showMe) {
                                return false;
                            }

                            scope.showMe = false;
                            scope.$digest();
                        });

                        scope.$watch('showMe', function(val) {
                            if (val) {
                                tooltip.css('display', 'block');
                                positionTooltip()
                            } else {
                                tooltip.css('display', 'none');
                            }
                        });
                        scope.showMe = false;
                        element.bind('click', toggle);
                    }
                }
            };
        }
    ])
    .directive("sticky", function($window, $timeout) {
        return {
            priority: 0,
            link: function(scope, element, attrs) {
                $timeout(function() {
                    var $win = angular.element($window);

                    if (scope._stickyElements === undefined) {
                        scope._stickyElements = [];

                        $win.bind("scroll.sticky", function(e) {
                            var pos = $win.scrollTop();
                            for (var i = 0; i < scope._stickyElements.length; i++) {

                                var item = scope._stickyElements[i];

                                if (!item.isStuck && pos > item.start) {
                                    item.element.css({
                                        'width': item.element.css('width'),
                                        'top': item.offsetTop
                                    });
                                    item.element.addClass("stuck");
                                    item.isStuck = true;

                                    if (item.placeholder) {
                                        item.placeholder = angular.element("<div></div>")
                                            .css({
                                                height: item.element.outerHeight() + "px"
                                            })
                                            .insertBefore(item.element);
                                    }
                                } else if (item.isStuck && pos < item.start) {
                                    item.element.css({
                                        'width': 'auto',
                                        'top': item.originTop
                                    });
                                    item.element.removeClass("stuck");
                                    item.isStuck = false;

                                    if (item.placeholder) {
                                        item.placeholder.remove();
                                        item.placeholder = true;
                                    }
                                }
                            }
                        });

                        var recheckPositions = function() {
                            for (var i = 0; i < scope._stickyElements.length; i++) {
                                var item = scope._stickyElements[i];
                                if (!item.isStuck) {
                                    item.start = item.element.offset().top;
                                } else if (item.placeholder) {
                                    item.start = item.placeholder.offset().top;
                                }
                            }
                        };
                        $win.bind("load", recheckPositions);
                        $win.bind("resize", recheckPositions);
                    }

                    var offsetTop = attrs.offsetTop || 0;
                    var item = {
                        element: element,
                        isStuck: false,
                        placeholder: attrs.usePlaceholder !== undefined,
                        start: element.offset().top - offsetTop,
                        offsetTop: +offsetTop,
                        originTop: element.css('top')
                    };

                    scope._stickyElements.push(item);
                }, 200);
            }
        };
    });