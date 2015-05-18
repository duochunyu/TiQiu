angular.module('tiqiu')
    .factory('Auth', function($http, $q) {
        // var currentUser;

        function getCookie(name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';'); //把cookie分割成组  
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i]; //取得字符串  
                while (c.charAt(0) == ' ') { //判断一下字符串有没有前导空格  
                    c = c.substring(1, c.length); //有的话，从第二位开始取  
                }
                if (c.indexOf(nameEQ) == 0) { //如果含有我们要的name  
                    return unescape(c.substring(nameEQ.length, c.length)); //解码并截取我们要值  
                }
            }
            return false;
        }

        function setCookie(name, value, seconds) {
            seconds = seconds || 0; //seconds有值就直接赋值，没有为0，这个根php不一样。  
            var expires = "";
            if (seconds != 0) { //设置cookie生存时间  
                var date = new Date();
                date.setTime(date.getTime() + (seconds * 1000));
                expires = "; expires=" + date.toGMTString();
            }
            document.cookie = name + "=" + escape(value) + expires + "; path=/"; //转码并赋值  
        }

        return {
            login: function(user) {
                // document.domain = "tiqiu365.com";
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/Accounthandler.ashx',
                    method: "GET",
                    params: {
                        action: 'LoginB',
                        name: user.username,
                        pwd: user.password,
                        code: user.code
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            setCookie('__tiqiu_token__', response.data.Data.Token);
                            localStorage.setItem('__tiqiu_user__', JSON.stringify(response.data.Data));
                            deferred.resolve(response.data.Data);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            },
            isLoggedIn: function() {
                var token = getCookie('__tiqiu_token__');
                return !!token;
            },
            getCurrentUser: function() {
                var token = getCookie('__tiqiu_token__');
                var user = localStorage.getItem('__tiqiu_user__');
                user = user && JSON.parse(user);
                if (user && user.Token === token) {
                    return user;
                } else {
                    localStorage.removeItem('__tiqiu_user__');
                    return null;
                }
            }
        };
    })
    .factory('Book', function($http, Auth, $q) {
        return {
            getFieldList: function() {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/FieldHandler.ashx',
                    method: "GET",
                    params: {
                        action: 'GetFieldList',
                        token: user.Token,
                        pageindex: 0,
                        pagesize: 1000
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve(response.data.Data.ItemList);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            },
            getFieldItemList: function(fieldId) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/FieldHandler.ashx',
                    method: "GET",
                    params: {
                        action: 'GetFieldItemList',
                        token: user.Token,
                        AccountBID: user.ID,
                        id: fieldId
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve(response.data.Data);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            },
            getFieldItemScheduledList: function(fieldItem, start, end) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/FieldHandler.ashx',
                    method: "GET",
                    params: {
                        action: 'GetFieldItemScheduledList',
                        token: user.Token,
                        AccountBID: user.ID,
                        id: fieldItem.id,
                        //FieldItemID: fieldItem.id,
                        start: start,
                        end: end
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve(response.data.Data);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            },
            orderFreeTeam: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/OrderHandler.ashx',
                    method: "GET",
                    params: {
                        action: 'OrderFreeTeam',
                        token: user.Token,
                        AccountBID: user.ID,
                        scheduledId: data.scheduledId,
                        price: data.price,
                        priceUnit: data.priceUnit,
                        minPlayerCount: data.minPlayerCount
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve();
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });

                return deferred.promise;
            },
            orderNormal: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                data.token = user.Token;
                data.accountBId = user.ID;
                data.action = "OrderNormalByManager"
                $http({
                    url: 'http://api.tiqiu365.com/OrderHandler.ashx',
                    method: "GET",
                    params: data
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve();
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });

                return deferred.promise;
            },
            cancelOrder: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                data.token = user.Token;
                data.accountBId = user.ID;
                data.action = "CancelOrderByAccountB"
                $http({
                    url: 'http://api.tiqiu365.com/OrderHandler.ashx',
                    method: "GET",
                    params: data
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve();
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });

                return deferred.promise;
            },
            completeOrder: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                data.token = user.Token;
                data.accountBId = user.ID;
                data.action = "CheckInOrder"
                $http({
                    url: 'http://api.tiqiu365.com/OrderHandler.ashx',
                    method: "GET",
                    params: data
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve();
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });

                return deferred.promise;
            },
            joinFreeTeam: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                data.token = user.Token;
                data.action = "JoinFreeTeamByManager";
                data.MemberID = 0;
                data.accountBId = user.ID;

                $http({
                    url: 'http://api.tiqiu365.com/OrderHandler.ashx',
                    method: "GET",
                    params: data
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve();
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });

                return deferred.promise;
            },
            batchOrder: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                data.token = user.Token;
                data.action = "OrderBatchByManager";
                data.accountBId = user.ID;

                $http({
                    url: 'http://api.tiqiu365.com/OrderHandler.ashx',
                    method: "GET",
                    params: data
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve();
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });

                return deferred.promise;
            },
            auditOrder: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                data.token = user.Token;
                data.action = "AcceptOrder";
                data.accountBId = user.ID;

                $http({
                    url: 'http://api.tiqiu365.com/OrderHandler.ashx',
                    method: "GET",
                    params: data
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve();
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });

                return deferred.promise;
            }
        };
    })
    .factory("Customer", function($http, Auth, $q) {
        return {
            getMemberList: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/MemberHandler.ashx',
                    method: "GET",
                    params: {
                        action: 'GetMemberList',
                        token: user.Token,
                        phone: data.phone
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve(response.data.Data);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            },
            getTeamList: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/MemberHandler.ashx',
                    method: "GET",
                    params: {
                        action: 'GetTeamList',
                        token: user.Token,
                        memberId: data.memberId
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve(response.data.Data);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            }
        };
    })
    .factory('Schedule', function($http, Auth, $q) {
        return {
            getFieldRuleList: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();
                $http({
                    url: 'http://api.tiqiu365.com/FieldHandler.ashx',
                    method: "GET",
                    params: {
                        action: 'GetFieldRuleList',
                        token: user.Token,
                        fieldItemId: data.fieldItemId
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve(response.data.Data);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            },
            createFieldRuleList: function(data) {
                var user = Auth.getCurrentUser() || {};
                var deferred = $q.defer();

                /**
                 * The workhorse; converts an object to x-www-form-urlencoded serialization.
                 * @param {Object} obj
                 * @return {String}
                 */
                var param = function(obj) {
                    var query = '',
                        name, value, fullSubName, subName, subValue, innerObj, i;

                    for (name in obj) {
                        value = obj[name];

                        if (value instanceof Array) {
                            for (i = 0; i < value.length; ++i) {
                                subValue = value[i];
                                fullSubName = name + '[' + i + ']';
                                innerObj = {};
                                innerObj[fullSubName] = subValue;
                                query += param(innerObj) + '&';
                            }
                        } else if (value instanceof Object) {
                            for (subName in value) {
                                subValue = value[subName];
                                fullSubName = name + '[' + subName + ']';
                                innerObj = {};
                                innerObj[fullSubName] = subValue;
                                query += param(innerObj) + '&';
                            }
                        } else if (value !== undefined && value !== null)
                            query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
                    }

                    return query.length ? query.substr(0, query.length - 1) : query;
                };

                $http({
                    url: 'http://api.tiqiu365.com/FieldHandler.ashx',
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    transformRequest: function(data) {
                        return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
                    },
                    data: {
                        action: 'CreateFieldRuleList',
                        token: user.Token,
                        fieldItemId: data.fieldItemId,
                        ruleItems: JSON.stringify(data.ruleItems)
                    }
                })
                    .then(function(response) {
                        if (response.data && response.data.Result == 1) {
                            deferred.resolve(response.data.Data);
                        } else {
                            deferred.reject(response.data)
                        }
                    }, function(response) {
                        deferred.reject({
                            HelpMessage: '系统不可用'
                        });
                    });
                return deferred.promise;
            }
        };
    });