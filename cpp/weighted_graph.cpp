class weighted_graph
{
private:
public:
    ll node;
    vector<vector<pair<ll, ll>>> e; // pair:<to,cost>
    void add_edge(ll a, ll b, ll cost = 1)
    {
        e[a].push_back(make_pair(b, cost));
        e[b].push_back(make_pair(a, cost));
    }
    void add_directed_edge(ll from, ll to, ll cost = 1)
    {
        e[from].push_back(make_pair(to, cost));
    }
    vector<ll> dijkstra(ll start)
    {
        priority_queue<pair<ll, ll>, vector<pair<ll, ll>>, greater<pair<ll, ll>>> pq;
        pq.push(make_pair(0, start));
        vector<ll> cost(node, LLONG_MAX);
        cost[start] = 0;
        while (pq.size() > 0)
        {
            ll priority, cur;
            tie(priority, cur) = pq.top();
            pq.pop();
            if (cost[cur] < priority)
                continue;
            for (auto i : e[cur])
            { // pair<ll,ll> i:<to,cost>
                if (cost[cur] + i.second < cost[i.first])
                {
                    cost[i.first] = cost[cur] + i.second;
                    pq.push(make_pair(cost[cur] + i.second, i.first));
                }
            }
        }
        return cost;
    }
    weighted_graph(ll node_count)
    {
        node = node_count;
        e = vector<vector<pair<ll, ll>>>(node_count);
    }
};