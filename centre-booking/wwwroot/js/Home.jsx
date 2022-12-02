class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: ''
        };

        // binding that allows for callbacks when the form is submitted
        this.handleChange = this.handleChange.bind(this);
        this.handleLogin = this.handleLogin.bind(this);
    }

    handleChange({ target }) {
        this.setState({
            [target.name]: target.value
        });
    }

    handleLogin() {
        if (this.state.username === '' || this.state.password === '') {
            alert('Missing username or password!');
        } else {
            /*
            var request;
            if (window.XMLHttpRequest) {
                // New browsers
                request = new XMLHttpRequest();
            } else {
                // Old IE Browsers
                request = new ActiveXObject("Microsoft.XMLHTTP");
            }
            if (request != null) {
                request.open("POST", "/Home/IsAuthenticate", false);
                var params = "{Name: " + this.state.username + ", Password: " + this.state.password + "}";
                // DEBUG
                //console.log("Username: " + this.state.username + ", Password: " + this.state.password);
                alert("Username: " + this.state.username + ", Password: " + this.state.password);
                // DEBUG ENDS

                request.setRequestHeader("Content-Type", "application/json");
                request.onload = () => {
                    if (request.readyState == 4 && request.status == 200) {
                        alert("request success");
                        const response = JSON.parse(request.responseText);
                        alert(response.value);
                    }
                };
                request.send(params);
            } 
            */

            /*
            $.ajax({
                type: 'POST',
                url: '/Home/SomethingPost/',
                contentType: 'application/json',
                cache: true,
                data: JSON.stringify(
                {
                    "Name": this.state.username,
                    "Password": this.state.password
                }),
                success: function (data, textStatus, jQxhr)
                {
                    console.log("Welcome Admin");
                    alert("Welcome Admin!");
                }.bind(this),
                error: function (xhr, textStatus, error)
                {
                    console.log("error");
                    alert("Error!");
                    alert(xhr.responseText);
                }.bind(this)
            });
            */

            const data = {
                Name: this.state.username.toString(),
                Password: this.state.password.toString()
            };

            fetch(`/Home/IsAuthenticate`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data),
            })
                .then(response => response.json())
                .then((data) => {
                    alert('Welcome Admin: ', data);
                })
                .catch((error) => {
                    alert('Error: ', error);
                });
        }
    }

    render() {
        return (
            <div className="home-page">
                <h1>Centre Booking</h1>
                <br />
                <form>
                    <input
                        type="text"
                        id="username"
                        name="username"
                        placeholder="Username"
                        onChange={this.handleChange}
                    />
                    <br />
                    <input
                        type="password"
                        id="password"
                        name="password"
                        placeholder="Password"
                        onChange={this.handleChange}
                    />
                    <br />
                    <button
                        id="loginBtn"
                        onClick={this.handleLogin}
                    >
                        Login
                    </button>
                </form>
            </div>
        );
    }
}

class Menu extends React.Component {
    render() {
        return (
            <h1>Menu</h1>
        );
    }
}

ReactDOM.render(
    <Home />,
    document.getElementById('content')
);